using SemesterYear_Assignment3_caenders.Analysis;
using SemesterYear_Assignment3_caenders.Models;
using System.Security.Cryptography;

namespace SemesterYear_Assignment3_caenders.Data
{
    public static class Tweets
    {
        public static async Task<string?> GPT_GenerateTweet(ActorModel actor)
        {
            //  Prompt generation
            string actor_info = actor.Name;
            string gpt_instructions = "You are a twitter user. Respond to the prompt with a tweet about the actor provided. If you do not have any information on them, make something generic up. Keep it less than 240 characters.";

            //  Generate response
            return await GPT.GPT4_ResponseFromPrompt(actor_info, gpt_instructions);
        }
    }
}
