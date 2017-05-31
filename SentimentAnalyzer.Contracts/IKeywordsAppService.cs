namespace SentimentAnalyzer.Contracts
{
    public interface IKeywordsAppService
    {
        void AddKeyword(string keyword, string categoryCode);
    }
}
