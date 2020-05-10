using Common.DataBase.Emulators;
using Common.DataBase.Repositories;
using Common.RestApi;
using Common.RestApi.Validators;
using CSharpVersion7_3.Service;
using CSharpVersion7_3.Service.Attributes;
using CSharpVersion7_3.Service.Validators;
using System;

namespace CSharpVersion7_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client.Client(new WebAPIClient());

            client.PringUsersByRatingAsync(0, 0).Wait();
            Console.ReadKey();
        }
    }
}
