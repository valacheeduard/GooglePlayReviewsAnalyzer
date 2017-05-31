
namespace SentimentAnalyzer.Business.DTOs
{
    public class ReviewsOverviewDto
    {
        public string ApplicationName { get; set; }

        public long RotalReviews { get; set; }

        public long FiveStarsReviews { get; set; }

        public long FourStarsReviews { get; set; }

        public long ThreeStarsReviews { get; set; }

        public long TwoStarsReviews { get; set; }

        public long OneStarReviews { get; set; }
    }
}
