using System.Collections.Generic;
using SentimentAnalyzer.Domain;

namespace SentimentAnalyzer.Contracts
{
    public interface IKeywordsAppService
    {
        Keyword AddKeyword(string keyword, string categoryCode);

        IEnumerable<Keyword> GetAll();

        void Delete(string id);
    }
}
