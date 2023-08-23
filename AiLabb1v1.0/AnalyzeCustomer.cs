using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.AI.TextAnalytics;
using Azure.Core;


namespace AiLabb1v1._0
{
    public static class AnalyzeCustomer
    {
        public static string customerChat = string.Empty;
        private static string choosenCust = string.Empty;

        private static string cognitiveServiceKeyAnalyze = string.Empty;
        private static string cognitiveEndpointAnalyze = string.Empty;

        private static Dictionary<string, string> customerList = new Dictionary<string, string>();

        public static string CognitiveServiceKeyAnalyze
        {
            set 
            { 
                cognitiveServiceKeyAnalyze = value;
            }
        }
        public static string CognitiveEndpointAnalyze
        {
            set
            {
                cognitiveEndpointAnalyze = value;
            }
        }


        public static void addToDictionary(string key, string value)
        {
            if (customerList.ContainsKey(key))
            {
                customerList[key] += "---------------\n--Next Chat--\n" + value ; // update an already existing chat
            }
            else
            {
                customerList.Add(key, value); //add new customer-chat
            }
        }


        //choosing customer-chat
        public static void ChooseCustomerFromDictionary()
        {
            if (customerList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No customer added in chat..");
                Console.ResetColor();
                System.Threading.Thread.Sleep(1500);
            }
            else
            {
                while (true)
                {
                    Console.Clear();

                    foreach (KeyValuePair<string, string> customer in customerList)
                    {
                        string customerName = customer.Key;

                        // display all customers
                        Console.WriteLine($"Customer: {customerName}");
                    }

                    Console.WriteLine("\n____________________________");
                    Console.Write("Enter the name of Customer: ");
                    choosenCust = Console.ReadLine();
                    choosenCust = choosenCust.Trim();
                    if (choosenCust.Length == 0)
                    {
                        Console.Clear();
                    }
                    else
                    {
                        bool custfound = false;
                        foreach (KeyValuePair<string, string> customer in customerList)
                        {
                            if (choosenCust.ToLower() == customer.Key.ToLower())
                            {
                                custfound = true;
                                break;
                            }
                        }
                        if (custfound == true)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\n\nDid not found customer, try again..");
                            System.Threading.Thread.Sleep(2000);
                        }
                    }
                }

                choosenCust = choosenCust.ToLower();
                choosenCust = char.ToUpper(choosenCust[0]) + choosenCust.Substring(1); // first letter capital
                // call function for entering saved chat
                GetConversation(choosenCust);
            }
        }


        //Display customer conversation
        private static void GetConversation(string customerName)
        {
            Console.Clear();
            string customerValue = customerList[customerName].ToLower();
            Console.WriteLine("----------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Customer: {customerName}\nChat:\n--------\n{customerValue}");
            Console.ResetColor();
            Console.WriteLine("----------------");

            ///////////////////////////////////////////////////////////////
            // Get sentiment

            Uri endpoint = new(cognitiveEndpointAnalyze); 
            AzureKeyCredential key = new(cognitiveServiceKeyAnalyze); 
            TextAnalyticsClient client = new(endpoint, key);

            Response<DocumentSentiment> response = client.AnalyzeSentiment(customerValue);
            DocumentSentiment docSentiment = response.Value;

            Console.WriteLine($"Document sentiment is {docSentiment.Sentiment} with: ");
            Console.WriteLine($"  Positive confidence score: {docSentiment.ConfidenceScores.Positive}");
            Console.WriteLine($"  Neutral confidence score: {docSentiment.ConfidenceScores.Neutral}");
            Console.WriteLine($"  Negative confidence score: {docSentiment.ConfidenceScores.Negative}");

            Console.ReadKey();
        }
    }
}
