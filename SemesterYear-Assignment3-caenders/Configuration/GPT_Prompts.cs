using SemesterYear_Assignment3_caenders.Analysis;

namespace SemesterYear_Assignment3_caenders.Configuration
{
    public static class GPT_Prompts
    {
        /*
         * 
         * Good luck engineering these prompts, they're tough.
         * 
         */
        //public static readonly string sys_review = "Given a movie, respond with a simulated review of the movie. Your feelings to the movie: $SENTIMENT. If you cannot give a review, simply respond with a generic review with the provided sentiment that could apply to any movie but DO NOT include the sentiment provided verbatim.";
        public static readonly string sys_review = "You will be given a movie. In response, write a brief review about the movie from the perspective of someone who $SENTIMENT. If you do not know anything about the movie, just give a generic compliment to it.";
        public static readonly string sys_tweet = "You are a twitter user. Respond to the prompt with a tweet about the actor provided. If you do not have any information on them, make something generic up. Keep it less than 240 characters.";
    }
}
