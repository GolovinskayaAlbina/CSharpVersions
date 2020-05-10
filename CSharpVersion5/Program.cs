using Common.RestApi;
using System;

namespace CSharpVersion5
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
