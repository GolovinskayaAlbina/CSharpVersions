using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.DIContainers
{
    public class Container
    {
        private IDictionary<Type, Type> dictionary = new Dictionary<Type, Type>();
        public void Bind<T1, T2>() where T2 : T1
        {
            dictionary.Add(typeof(T1), typeof(T2));
        }

        public void Bind<T>()
        {
            Bind<T, T>();
        }

        public T2 Get<T1, T2>()
        {
            return (T2)Get(typeof(T1));
        }

        public object Get(Type type)
        {
            return CreateObject(type);
        }

        private object CreateObject(Type type)
        {
            if (!dictionary.ContainsKey(type))
            {
                throw new NotImplementedException($"Bind for type '{type}' not set");
            }
            type = dictionary[type];

            var constructors = type.GetConstructors();

            if (constructors.Length == 0)
            {
                throw new NotImplementedException($"'{type}' constructor");
            }

            var parameters = constructors.First()
                .GetParameters()
                .Select(_ => CreateObject(_.ParameterType))
                .ToArray();

            return Activator.CreateInstance(type, parameters);

        }
    }
}
