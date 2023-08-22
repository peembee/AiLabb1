using Microsoft.Extensions.Configuration;
using Azure;
using Azure.AI.Language.QuestionAnswering;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using AiLabb1v1._0;

namespace AILabb1
{
    public class Bot
    {
        private readonly IConfiguration configuration;

        // saves key and location for Q-A
        string cognitiveServiceKey = string.Empty;
        string CognitiveEndpoint = string.Empty;
        string projectName = string.Empty;

        // saves key and location for Speech
        string CognitiveServiceKeySpeech = string.Empty;
        string CognitiveLocationSpeech = string.Empty;


        // collecting the data from appsettings to variables..
        public Bot(IConfiguration configuration)
        {
            this.configuration = configuration;
            cognitiveServiceKey = this.configuration["CognitiveServiceKeyQA"];
            CognitiveEndpoint = this.configuration["CognitiveEndpointQA"];
            projectName = this.configuration["projectNameQA"];
            CognitiveServiceKeySpeech = this.configuration["CognitiveServiceKeySpeech"];
            CognitiveLocationSpeech = this.configuration["CognitiveLocationSpeech"];
        }


        public void prepareQuestion(string customerName, bool botToSpeech)
        {
            string question = string.Empty;

            //variable for saving chat from customer
            string conversationChat = string.Empty;

            // enter chat with azure
            Console.WriteLine("--------");
            Console.WriteLine("Exit chat: enter 'quit' or '0'");
            Console.WriteLine("--------\n-\n");
            sendQuestion("whats your name", botToSpeech);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"\n{customerName}: ");
                Console.ResetColor();
                question = Console.ReadLine();
                question = question.Trim();

                if (question.ToLower() == "quit" || question == "0")
                {
                    Console.Clear();

                    // adding customerQuestions to dictionary in class: AnalyzeCustomer
                    AnalyzeCustomer.addToDictionary(customerName, conversationChat);

                    Console.WriteLine("\n- - - - - - - - - - -");
                    Console.WriteLine("Thank you for chatting, redirecting to start-page");
                    System.Threading.Thread.Sleep(1000);
                    break;
                }
                else if (question.Length == 0)
                {
                    string noInput = "\nYou need to type something..";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(noInput);
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(500);
                    for (int i = 0; i < noInput.Length; i++)
                    {
                        System.Threading.Thread.Sleep(20);
                        Console.Write("\b \b");
                    }
                    // Delete the upper-Row
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.CursorTop -= 3; // Go 3 rows back up
                    Console.CursorLeft = 0;
                }
                else
                {
                    // adding question to conversationChat
                    conversationChat += "-" + question + " \n\n";

                    //Send question
                    sendQuestion(question, botToSpeech);
                }
            }
        }


        private async Task sendQuestion(string question, bool botToSpeech)
        {
            Uri endpoint = new Uri($"{CognitiveEndpoint}");
            AzureKeyCredential credential = new AzureKeyCredential($"{cognitiveServiceKey}");
            string getProjectName = $"{projectName}";
            string deploymentName = "production";

            string answerFromBot = string.Empty;

            //Configure Q-A
            QuestionAnsweringClient client = new QuestionAnsweringClient(endpoint, credential);
            QuestionAnsweringProject project = new QuestionAnsweringProject(getProjectName, deploymentName);

            Response<AnswersResult> response = client.GetAnswers(question, project);

            foreach (KnowledgeBaseAnswer answer in response.Value.Answers)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Customer-Service: ");
                Console.ResetColor();
                Console.Write($"{answer.Answer}");
                answerFromBot = answer.Answer;
            }

            if (botToSpeech == true) // if settings: bot-Spekaer == true, send it to bot-speaker
            {
                await botSpeech(answerFromBot);
            }
        }


        private async Task botSpeech(string answer)
        {
            //Configure speech service
            var speechConfig = SpeechConfig.FromSubscription(CognitiveServiceKeySpeech, CognitiveLocationSpeech);

            // The language of the voice that speaks.
            speechConfig.SpeechSynthesisVoiceName = "en-US-JennyNeural";

            // Synthesize spoken output
            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
            {
                // Get text from the botAsnwerText and synthesize to the default speaker.
                var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(answer);
            }
        }
    }
}
