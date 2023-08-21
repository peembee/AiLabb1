using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;


namespace AILabb1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            CustomerService customerService = new CustomerService(configuration);

            customerService.RunCustomerService();

            Console.WriteLine("\n\n- - - - - - - - - - -");
            Console.WriteLine("Key for Close customer-service ");
            Console.ReadKey();
        }
    }
}