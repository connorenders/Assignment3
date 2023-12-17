using SemesterYear_Assignment3_caenders.Data;
using SemesterYear_Assignment3_caenders.Models;
using System.Collections.Generic;

namespace SemesterYear_Assignment3_caenders.Testing
{
    //  Ignore this, it's for testing

    public static class DataGen
    {
        //  Test gen
        public static async Task<List<(string review, double sentiment)>> GenerateReviews(string movie_name, uint count)
        {
            var results = new List<(string review, double sentiment)> ();

            MovieModel movie = new();
            movie.Title = movie_name;

            for (int i = 0; i < count; i++)
            {
                //  Generate review
                string? review = await Reviews.GPT_GenerateMovieReview(movie);
                if(review == null) { review = "FAILED"; }

                //  Generate sentiment analysis
                double sentiment = Analysis.Sentiment.AnalyzeSentiment(review).Compound;

                results.Add((review, sentiment));
            }

            return results;
        }

        public static async Task<List<(string review, double sentiment)>> GenerateTweets(string actor_name, uint count)
        {
            var results = new List<(string review, double sentiment)>();

            ActorModel actor = new();
            actor.Name = actor_name;

            for (int i = 0; i < count; i++)
            {
                //  Generate review
                string? tweet = await Tweets.GPT_GenerateTweet(actor);
                if (tweet == null) { tweet = "FAILED"; }

                //  Generate sentiment analysis
                double sentiment = Analysis.Sentiment.AnalyzeSentiment(tweet).Compound;

                results.Add((tweet, sentiment));
            }

            return results;
        }
    }
}
