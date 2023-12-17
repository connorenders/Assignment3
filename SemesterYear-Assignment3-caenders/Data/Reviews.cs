using SemesterYear_Assignment3_caenders.Analysis;
using SemesterYear_Assignment3_caenders.Configuration;
using SemesterYear_Assignment3_caenders.Models;
using System.Security.Cryptography;

namespace SemesterYear_Assignment3_caenders.Data
{
    public static class Reviews
    {
        //  The annoying OpenAI content policy refuses to produce negative reviews
        public enum Intended_Sentiment
        {
            LOVED_IT,
            LIKED_IT,
            SORT_OF_LIKED_IT,
            WASNT_A_FAN_BUT_CAN_APPRECIATE_IT
        };

        public static async Task<string?> GPT_GenerateMovieReview(MovieModel movie, Intended_Sentiment? sentiment = null)
        {
            //  Random sentiment if none provided
            if (sentiment == null)
            {
                int sentimentCount = Enum.GetValues(typeof(Intended_Sentiment)).Length;
                sentiment = (Intended_Sentiment)RandomNumberGenerator.GetInt32(0, sentimentCount);
            }

            //  Prompt generation
            string movie_info = movie.Title;
            string gpt_instructions = GPT_Prompts.sys_review.Replace("$SENTIMENT", (sentiment.ToString().Replace('_', ' ').ToLower()));

            //  Generate response
            string result = await GPT.GPT4_ResponseFromPrompt(movie_info, gpt_instructions);

            //  Handle response
            if (result == null) return null;

            //  Remove verbatim sentiment. No idea why ChatGPT leaves it in.
            result = result.Replace(sentiment.ToString(), "");

            //  Failed to fulfull
            //if(result.Contains("I'm sorry"))
            //{
            //    return null;
            //}

            return result;
        }
    }
}
