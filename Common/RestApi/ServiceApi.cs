using Common.DataBase.Emulators;
using Common.DataBase.Repositories;
using Common.RestApi.Attributes;
using Common.RestApi.Emulators.Controllers;
using Common.RestApi.Validators;
using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.RestApi
{
    class ServiceApi
    {
        public static async Task<Res> CallMethodAsync<Res, Req>(string path, Req request)
        {
            string controller;
            string action;
            Type validatorType;

            if (TryParseApiPath(path, out controller, out action))
            {
                var controllerType = GetControllerTypeByName(controller, out validatorType);

                if (controllerType != null && validatorType != null)
                {
                    bool isExceptionFilter;
                    MethodInfo actionInfo;
                    Type attributeType;

                    if (TryGetAction(controllerType, action, out actionInfo, out attributeType, out isExceptionFilter))
                    {
                        var controllerArgs = new object[] { new DBRepository(new DbConnectionEmulatorFactory()), Activator.CreateInstance(validatorType) };
                        var controllerObject = Activator.CreateInstance(actionInfo.DeclaringType, controllerArgs);
                        if (isExceptionFilter)
                        {
                            var filterObject = (ExceptionFilterAttribute)Activator.CreateInstance(attributeType);
                            try
                            {
                                return await (Task<Res>)actionInfo.Invoke(controllerObject, new object[] { request });
                            }
                            catch(Exception ex)
                            {
                                filterObject.OnException(ex);
                            }
                        }
                        else
                        {
                            return await (Task<Res>)actionInfo.Invoke(controllerObject, new object[] { request });
                        }
                    }
                }
            }

            Response.ThrowResponseException(HttpStatusCode.NotFound);
            return default(Res);
        }
        static Type GetControllerTypeByName(string controller, out Type validatorType)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(_ => _.GetTypes());

            validatorType = types.Where(_ => _.IsClass && typeof(IValidator).IsAssignableFrom(_))
                .FirstOrDefault();

            return types.Where(_ => _.IsClass && _.IsSubclassOf(typeof(ControllerEmulator)))
                .Where(_ => _.Name.ToLower() == controller)
                .FirstOrDefault();
        }
        static bool TryGetAction(Type controllerType, string action, out MethodInfo info, out Type attributeType, out bool isExceptionFilter)
        {
            isExceptionFilter = false;
            attributeType = null;
            info = controllerType.GetMethods()
                .Where(_ => _.Name.ToLower() == action)
                .FirstOrDefault();

            if (info != null)
            {
                var attribute = info.GetCustomAttributes(typeof(ExceptionFilterAttribute), true)
                    .FirstOrDefault();

                isExceptionFilter = attribute != null;
                if (isExceptionFilter)
                {
                    attributeType = attribute.GetType();
                }
                return true;
            }

            return false;
        }

        static bool TryParseApiPath(string path, out string controller, out string action)
        {
            controller = string.Empty;
            action = string.Empty;

            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            var apiParts = path.Split( '/' );
            if (apiParts.Length == 0)
            {
                return false;
            }
            controller = apiParts[0].ToLower() + "controller";
            if (apiParts.Length == 1)
            {
                action = "index";
            }
            else
            {
                action = apiParts[1].ToLower();
            }

            return true;
        }
    }
}
