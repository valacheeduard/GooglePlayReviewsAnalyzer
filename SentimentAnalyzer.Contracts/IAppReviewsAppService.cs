
using SentimentAnalyzer.Domain;

namespace SentimentAnalyzer.Contracts
{
    public interface IAppReviewsAppService
    {
        ReviewsOverviewDto GetReviews(string appId);
    }
}
