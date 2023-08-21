﻿using Microsoft.Extensions.Configuration;
using Azure;
using Azure.AI.Language.QuestionAnswering;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

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

        public Bot(IConfiguration configuration)
        {
            this.configuration = configuration;
            cognitiveServiceKey = this.configuration["CognitiveServiceKeyQA"];
            CognitiveEndpoint = this.configuration["CognitiveEndpointQA"];
            projectName = this.configuration["projectNameQA"];
            CognitiveServiceKeySpeech = this.configuration["CognitiveServiceKeySpeech"];
            CognitiveLocationSpeech = this.configuration["CognitiveLocationSpeech"];
        }

        public void prepareQuestion(string customerName)
        {
            string question = string.Empty;
            // enter chat with azure
            Console.WriteLine("--------");
            Console.WriteLine("Exit chat: enter 'quit' or '0'");
            Console.WriteLine("--------\n\n");
            sendQuestion("whats your name");

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
                    sendQuestion(question);
                }
            }
        }

        private async void sendQuestion(string question)
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

            await botSpeech(answerFromBot);
        }

        private async Task botSpeech(string answer)
        {
            //Configure speech service
            var speechConfig = SpeechConfig.FromSubscription(CognitiveServiceKeySpeech, CognitiveLocationSpeech);

            // The language of the voice that speaks.
            speechConfig.SpeechSynthesisVoiceName = "en-GB-RyanNeural";

            // Synthesize spoken output
            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
            {
                // Get text from the console and synthesize to the default speaker.
                var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(answer);
            }
        }
    }
}
