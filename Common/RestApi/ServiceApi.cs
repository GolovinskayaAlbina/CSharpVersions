using Common.DIContainers;
using Common.RestApi.Attributes;
using Common.RestApi.Emulators.Controllers;
using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.RestApi
{
    class ServiceApi
    {
        private readonly Container _container;

        public ServiceApi(Container container)
        {
            _container = container;
        }

        public async Task<Res> CallMethodAsync<Res, Req>(string path, Req request)
        {
            //7.0 Tuples <code>(T1 t1,T2 t2) obj =  Do()</code>
            (bool isSuccess, string controllerName, string actionName) parsedPath = TryParseApiPath(path);
            if (parsedPath.isSuccess)
            {
                var controllerType = GetControllerTypeByName(parsedPath.controllerName);

                if (controllerType != null)
                {
                    //7.0 Tuples <code>(T1 t1,T2 t2) obj =  Do()</code>
                    (bool isSuccess, MethodInfo actionInfo, Type attributeType, bool isExceptionFilter) action = TryGetAction(controllerType, parsedPath.actionName);
                    if (action.isSuccess)
                    {
                        var controllerObject = _container.Get(controllerType);

                        try
                        {
                            return await (Task<Res>)action.actionInfo.Invoke(controllerObject, new object[] { request });
                        }
                        catch (Exception ex)
                        {
                            if (action.isExceptionFilter)
                            {
                                var filterObject = (ExceptionFilterAttribute)Activator.CreateInstance(action.attributeType);
                                filterObject.OnException(ex);
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
            }

            Response.ThrowResponseException(HttpStatusCode.NotFound);
            return default;
        }

        //7.0 Tuples <code>(T1,T2) Do(){return (new T1(),new T2())}</code>
        static Type GetControllerTypeByName(string controllerName)
        {
           return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(_ => _.GetTypes()).Where(_ => _.IsClass && _.IsSubclassOf(typeof(ControllerEmulator)))
                .Where(_ => _.Name.ToLower() == controllerName)
                .FirstOrDefault();
        }

        //7.0 Tuples <code>(T1,T2) Do(){return (new T1(),new T2())}</code>
        static (bool, MethodInfo, Type, bool) TryGetAction(Type controllerType, string action)
        {
            var info = controllerType.GetMethods()
                .Where(_ => _.Name.ToLower() == action)
                .FirstOrDefault();

            if (info != null)
            {
                var attribute = info.GetCustomAttributes(typeof(ExceptionFilterAttribute), true)
                    .FirstOrDefault();

                return (true, info, attribute?.GetType(), attribute != null);
            }

            return (false, null, null, false);
        }

        //7.0 Tuples <code>(T1,T2) Do(){return (new T1(),new T2())}</code>
        static (bool,string,string) TryParseApiPath(string path)
        {
            var controller = string.Empty;
            var action = string.Empty;

            if (string.IsNullOrEmpty(path))
            {
                return (false, controller, action);
            }

            var apiParts = path.Split( '/' );
            if (apiParts.Length == 0)
            {
                return (false, controller, action);
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

            return (true, controller, action);
        }
    }
}
