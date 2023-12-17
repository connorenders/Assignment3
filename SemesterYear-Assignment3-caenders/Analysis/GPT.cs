using SemesterYear_Assignment3_caenders.Configuration;
using OpenAI_API;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using OpenAI_API.Models;

namespace SemesterYear_Assignment3_caenders.Analysis
{
    public static class GPT
    {
        //  Azure AI Client
        private static OpenAIAPI _api = new(APIKeys.OpenAI_Key);

        //  Request
        public static async Task<string?> GPT4_ResponseFromPrompt(string prompt, string system_message, double temperature = 0.5)
        {
            try
            {
                // Create a conversation
                var chat = _api.Chat.CreateConversation();
                chat.Model = new Model("gpt-3.5-turbo-1106");
                chat.RequestParameters.Temperature = temperature;

                // Send the prompt to the conversation
                chat.AppendSystemMessage(system_message);
                chat.AppendUserInput(prompt);

                // Return the text of the result
                string response = await chat.GetResponseFromChatbotAsync();
                return response.ToString();
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Debug.WriteLine("[GPT] Error while fetching response from OpenAI: " + ex.Message);
                return null;
            }
        }
    }
}
