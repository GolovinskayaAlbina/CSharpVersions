using System;

namespace Common.RestApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class ExceptionFilterAttribute : Attribute
    {
        public abstract void OnException(Exception exception);
    }
}
