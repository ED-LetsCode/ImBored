using System.Text.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImBored
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ImBored();
        }

        static void ImBored()
        {
            bool consoleIsRunning = true;
            while (consoleIsRunning)
            {
                PrintHeader();
                switch (Console.ReadLine().ToLower())
                {
                    case "1":
                        WhatCanIDo();
                        break;

                    case "2":
                        WhatsMyIpAdress();
                        break;

                    case "3":
                        PredictGender();
                        break;

                    case "4":
                        GetSomeJokes();
                        break;

                    case "c":
                        Console.Clear();
                        break;

                    case "x":
                        consoleIsRunning = false;
                        break;

                    default:
                        Console.WriteLine("Choose a Menu Point");
                        break;
                }
            }
        }

        /// <summary>
        /// Get response from a requestURL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <returns>a Task with t datatype</t></returns>
        static async Task<T> GetResponse<T>(string requestUrl) => JsonSerializer.Deserialize<T>(await new HttpClient().GetStringAsync(requestUrl));

        /// <summary>
        /// Get some random Jokes
        /// </summary>
        static void GetSomeJokes()
        {
            try
            {
                var response = GetResponse<Jokes>("https://v2.jokeapi.dev/joke/Any").Result;
                Console.WriteLine($"category: {response.category}\n" +
                            $"joke question: {response.setup}\n" +
                            $"joke answer: {response.delivery}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Print some activities in the Console
        /// </summary>
        static void WhatCanIDo()
        {
            try
            {
                var response = GetResponse<WhatCanIDoDTO>("http://www.boredapi.com/api/activity/").Result;
                Console.WriteLine($"{response.activity}\n" +
                            $"type: {response.type}\n" +
                            $"link: {response.link}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Prints your'e Ip Adress in the Console
        /// </summary>
        static void WhatsMyIpAdress()
        {
            try
            {
                var response = GetResponse<WhatsMyIpAdressDTO>("https://api.ipify.org/?format=json").Result;
                Console.WriteLine($"ip Adress: {response.ip}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Predicts the gender of a person based on their name and prints in the Console
        /// </summary>
        static void PredictGender()
        {
            try
            {
                Console.WriteLine("Enter a Name");
                var response = GetResponse<PredictGenderDTO>($"https://api.genderize.io/?name={Console.ReadLine()}").Result;
                Console.WriteLine($"name: {response.name}\n" +
                            $"gender: {response.gender}\n" +
                            $"amount: {response.count}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("██╗██  ███╗   ███╗    ██████╗  ██████╗ ██████╗ ███████╗██████╗\n" +
                              "██║    ████╗ ████║    ██╔══██╗██╔═══██╗██╔══██╗██╔════╝██╔══██╗\n" +
                              "██║    ██╔████╔██║    ██████╔╝██║   ██║██████╔╝█████╗  ██║  ██║\n" +
                              "██║    ██║╚██╔╝██║    ██╔══██╗██║   ██║██╔══██╗██╔══╝  ██║  ██║\n" +
                              "██║    ██║ ╚═╝ ██║    ██████╔╝╚██████╔╝██║  ██║███████╗██████╔╝\n" +
                              "╚═╝    ╚═╝     ╚═╝    ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚══════╝╚═════╝\n\n" +
                              "What can I do?                                       [1]\n" +
                              "What's my Ip Adress?                                 [2]\n" +
                              "Predict the gender of a person based on their name   [3]\n" +
                              "Get some random Jokes                                [4]\n" +
                              "Clear Console                                        [c]\n" +
                              "Exit Console                                         [x]\n");
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
