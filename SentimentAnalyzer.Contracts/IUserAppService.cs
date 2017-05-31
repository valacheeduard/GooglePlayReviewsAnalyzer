using SentimentAnalyzer.Domain;

namespace SentimentAnalyzer.Contracts
{
    public interface IUserAppService
    {
        User GetUserByEmailAndPassword(string username, string password);
    }
}
