using Common;
using Common.DataBase.Emulators;
using Common.DataBase.Repositories;
using Common.DIContainers;
using Common.RestApi;
using CSharpVersion5.Service;
using CSharpVersion5.Service.Validators;
using System;

namespace CSharpVersion5
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client.Client(new WebAPIClient(CreateContainer()));

            client.PringUsersByRatingAsync(0, 0).Wait();
            Console.ReadKey();
        }

        static Container CreateContainer()
        {
            var container = new Container();
            container.Bind<ServiceController>();
            container.Bind<IValidator, Validator>();
            container.Bind<IRepository, DBRepository>();
            container.Bind<IDbConnectionFactory, DbConnectionEmulatorFactory>();

            return container;
        }
    }
}
