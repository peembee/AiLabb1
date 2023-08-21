using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AILabb1
{
    public class CustomerService
    {
        string CustomerName = string.Empty;
        Bot bot;
        public CustomerService(IConfiguration configuration)
        {
            bot = new Bot(configuration);

            do
            {
                Console.Clear();
                Console.WriteLine(" (Min Two Characters) ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Hi! Please enter your name: ");
                Console.ResetColor();
                CustomerName = Console.ReadLine();
                CustomerName = CustomerName.Trim();
                if (CustomerName.Length > 0)
                {
                    CustomerName = CustomerName.ToLower();
                    CustomerName = char.ToUpper(CustomerName[0]) + CustomerName.Substring(1); // first letter capital
                }
            } while (CustomerName.Length < 2);
        }

        public void RunCustomerService()
        {
            bool closeSupport = false;

            while (closeSupport == false)
            {
                string userInput = string.Empty;
                Console.Clear();
                Console.WriteLine("------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Welcome to the customer support");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($" {CustomerName}");
                Console.ResetColor();
                Console.WriteLine("\n------------------------\n");
                Console.WriteLine("#1: lägg till AI-tjänst");
                Console.WriteLine("#2: Customer Service chat");
                Console.WriteLine("#0: Close Customer Service");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Enter option # ");
                Console.ResetColor();
                userInput = Console.ReadLine();

                if (userInput == "0")
                {
                    closeSupport = true;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n** Thank you for contacting us at the ultimate human support **\n\n");
                    Console.ResetColor();
                }
                else if (userInput == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Användar val 1");
                    Console.ReadKey();
                }
                else if (userInput == "2")
                {
                    Console.Clear();
                    enterChat();
                }
                else
                {
                    Console.WriteLine("Enter a valid Option");
                    System.Threading.Thread.Sleep(500);
                }
            }
        }


        private void enterChat()
        {
            Console.Write("\n\nLoading ");
            System.Threading.Thread.Sleep(200);
            Console.Write("c");
            System.Threading.Thread.Sleep(200);
            Console.Write("h");
            System.Threading.Thread.Sleep(200);
            Console.Write("a");
            System.Threading.Thread.Sleep(200);
            Console.Write("t");

            for (int i = 0; i < 2; i++)
            {
                adDots();
                System.Threading.Thread.Sleep(300);
                deleteDots();
            }
            connected();

            bot.prepareQuestion(CustomerName);
            // ---- Local methods for loading..----
            void adDots()
            {
                for (int i = 0; i < 3; i++)
                {
                    System.Threading.Thread.Sleep(300);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(".");
                    Console.ResetColor();
                }
            }
            void deleteDots()
            {
                System.Threading.Thread.Sleep(20);
                Console.Write("\b \b");
                System.Threading.Thread.Sleep(20);
                Console.Write("\b \b");
                System.Threading.Thread.Sleep(20);
                Console.Write("\b \b");
                System.Threading.Thread.Sleep(20);
            }
            void connected()
            {
                for (int i = 0; i < 4; i++)
                {
                    deleteDots();
                }
                Console.ForegroundColor = ConsoleColor.Green;

                System.Threading.Thread.Sleep(50);
                Console.Write("W");
                System.Threading.Thread.Sleep(50);
                Console.Write("e ");

                System.Threading.Thread.Sleep(50);
                Console.Write("A");
                System.Threading.Thread.Sleep(50);
                Console.Write("r");
                System.Threading.Thread.Sleep(50);
                Console.Write("e ");

                System.Threading.Thread.Sleep(50);
                Console.Write("C");
                System.Threading.Thread.Sleep(50);
                Console.Write("o");
                System.Threading.Thread.Sleep(50);
                Console.Write("n");
                System.Threading.Thread.Sleep(50);
                Console.Write("n");
                System.Threading.Thread.Sleep(50);
                Console.Write("e");
                System.Threading.Thread.Sleep(50);
                Console.Write("c");
                System.Threading.Thread.Sleep(50);
                Console.Write("t");
                System.Threading.Thread.Sleep(50);
                Console.Write("e");
                System.Threading.Thread.Sleep(50);
                Console.Write("d");
                System.Threading.Thread.Sleep(400);
                Console.WriteLine();
                Console.ResetColor();
            }
            // - - - - - - - - - - - - - - - - - - -
        }
    }
}
