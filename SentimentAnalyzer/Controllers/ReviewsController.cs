using System.Net;
using System.Net.Http;
using System.Web.Http;
using SentimentAnalyzer.Business;
using SentimentAnalyzer.Contracts;

namespace SentimentAnalyzer.Controllers
{
    public class ReviewsController : ApiController
    {
        private readonly IAppReviewsAppService appService;

        public ReviewsController()
        {
            appService = new ReviewsAppService();
        }

        [HttpGet]
        [Route("api/reviews")]
        public HttpResponseMessage AnalysisReviews(string appId)
        {
            var analysis = appService.GetReviews(appId);

            return Request.CreateResponse(HttpStatusCode.OK, analysis);
        }
    }
}