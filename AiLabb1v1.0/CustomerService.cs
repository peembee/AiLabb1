using AiLabb1v1._0;
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
        bool botToSpeech = false;
        string CustomerName = string.Empty;

        Bot bot;

        //Send appsettings-data to bot-Class
        public CustomerService(IConfiguration configuration)
        {
            bot = new Bot(configuration);
            enterCustomer();
        }


        private void enterCustomer()
        {
            AnalyzeCustomer.customerChat = string.Empty;
            CustomerName = string.Empty;
            botToSpeech = false;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n___________________________");
                Console.Write("Analyze:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Scout\n");
                Console.ResetColor();
                Console.WriteLine("Exit application, press 0");
                Console.WriteLine("--");
                Console.WriteLine(" (Min Two Characters) ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Hi! Please enter your name: ");
                Console.ResetColor();
                CustomerName = Console.ReadLine();
                CustomerName = CustomerName.Trim();

                if (CustomerName == "0")
                {
                    break;
                }
                else if (CustomerName.Length > 1)
                {
                    CustomerName = CustomerName.ToLower();
                    CustomerName = char.ToUpper(CustomerName[0]) + CustomerName.Substring(1); // first letter capital
                    if (CustomerName.ToLower() == "scout")
                    {
                        AnalyzeCustomer.ChooseCustomerFromDictionary();
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (CustomerName != "0")
            {
                RunCustomerService();
            }
        }


        // Run main-program
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
                Console.WriteLine("#1: Voice-Settings");
                Console.WriteLine("#2: Customer Service chat");
                Console.WriteLine("#0: Close your Customer Service");
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
                    Console.WriteLine("Redirecting..");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(2000);
                }
                else if (userInput == "1")
                {
                    speakerChoice();
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
            enterCustomer();
        }


        // settings for bot-speaker
        private void speakerChoice()
        {
            while (true)
            {
                string inputFromUser = string.Empty;
                Console.Clear();
                Console.WriteLine("Would you like that Customer Service speak what they wrote to you?");
                Console.WriteLine("#1 = Yes");
                Console.WriteLine("#2 = No");
                Console.Write("Your Choice: ");
                inputFromUser = Console.ReadLine();
                inputFromUser = inputFromUser.Trim();
                if (inputFromUser.Length == 0)
                {
                    Console.WriteLine("You need to type something..");
                    System.Threading.Thread.Sleep(1000);
                }
                else if (inputFromUser == "1" || inputFromUser.ToLower() == "yes")
                {
                    botToSpeech = true;
                    break;
                }
                else if (inputFromUser == "2" || inputFromUser.ToLower() == "no")
                {
                    botToSpeech = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Make sure you're enter a valid option..");
                    System.Threading.Thread.Sleep(1000);
                }
            }
            Console.WriteLine("Redirecting to Home..");
            System.Threading.Thread.Sleep(1500);
            RunCustomerService();
        }


        // loading scenes for open chat..
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

            bot.prepareQuestion(CustomerName, botToSpeech);


            //--------------------------------------------------------------
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
            //----------------------
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
            //----------------------
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
