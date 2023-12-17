using VaderSharp2;

namespace SemesterYear_Assignment3_caenders.Analysis
{
    public class Sentiment
    {
        public static SentimentAnalysisResults AnalyzeSentiment(string text)
        {
            // Initialize the analyzer
            var analyzer = new SentimentIntensityAnalyzer();

            // Analyze the sentiment of the text
            var results = analyzer.PolarityScores(text);

            // Returning the results which contain the scores
            return results;
        }
    }
}
