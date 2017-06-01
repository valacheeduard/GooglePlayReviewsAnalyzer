using SentimentAnalyzer.Domain;

namespace SentimentAnalyzer.Contracts
{
    public interface ISentimentAppService
    {
        SentimentAnalysisResult AnalyzeReviews(string appId);

        string DownloadAnalysis(string appId);
    }
}
