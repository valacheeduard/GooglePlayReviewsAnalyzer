using System;
using System.Linq;
using HtmlAgilityPack;
using SentimentAnalyzer.Business.Extensions;
using SentimentAnalyzer.Contracts;
using SentimentAnalyzer.Domain;

namespace SentimentAnalyzer.Business
{
    public class ReviewsAppService : IAppReviewsAppService
    {
        public ReviewsOverviewDto GetReviews(string appId)
        {
            string url = "https://play.google.com/store/apps/details?id=" + appId + "&hl=en";

            HtmlWeb web = new HtmlWeb();

            HtmlDocument document = web.Load(url);

            var appName = document.GetDivsByClass("id-app-title").First().InnerText;
            var totalReviews = document.GetSpansByClass("reviews-num").First().InnerText;
            var fiveStarsReviews = document.GetDivsByClass("rating-bar-container five").First().InnerText;
            var fourStarsReviews = document.GetDivsByClass("rating-bar-container four").First().InnerText;
            var threeStarsReviews = document.GetDivsByClass("rating-bar-container three").First().InnerText;
            var twoStarsReviews = document.GetDivsByClass("rating-bar-container two").First().InnerText;
            var oneStarsReviews = document.GetDivsByClass("rating-bar-container one").First().InnerText;

            var fiveStarsReviewsNumber = fiveStarsReviews.Substring(fiveStarsReviews.IndexOf("5", StringComparison.Ordinal) + 1).Replace(",", "");
            var fourStarsReviewsNumber = fourStarsReviews.Substring(fourStarsReviews.IndexOf("4", StringComparison.Ordinal) + 1).Replace(",", "");
            var threeStarsReviewsNumber = threeStarsReviews.Substring(threeStarsReviews.IndexOf("3", StringComparison.Ordinal) + 1).Replace(",", "");
            var twoStarsReviewsNumber = twoStarsReviews.Substring(twoStarsReviews.IndexOf("2", StringComparison.Ordinal) + 1).Replace(",", "");
            var oneStarsReviewsNumber = oneStarsReviews.Substring(oneStarsReviews.IndexOf("1", StringComparison.Ordinal) + 1).Replace(",", "");

            return new ReviewsOverviewDto
            {
                ApplicationName = appName,
                TotalReviews = long.Parse(totalReviews.Replace(",", "")),
                FiveStarsReviews = long.Parse(fiveStarsReviewsNumber),
                FourStarsReviews = long.Parse(fourStarsReviewsNumber),
                ThreeStarsReviews = long.Parse(threeStarsReviewsNumber),
                TwoStarsReviews = long.Parse(twoStarsReviewsNumber),
                OneStarReviews = long.Parse(oneStarsReviewsNumber)
            };
        }
    }
}
