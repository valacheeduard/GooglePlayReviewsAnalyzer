using System.Net;
using System.Net.Http;
using System.Web.Http;
using SentimentAnalyzer.Business;
using SentimentAnalyzer.Contracts;

namespace SentimentAnalyzer.Controllers
{
    public class AnalysisController : ApiController
    {
        private IAppReviewsAppService appReviewsLogic;

        public AnalysisController()
        {
            appReviewsLogic = new ReviewsAppService();
        }


        [HttpGet]
        [Route("api/analysis")]
        public HttpResponseMessage AnalysisReviews(string appId)
        {
            var analysis = appReviewsLogic.GetReviews(appId);

            return Request.CreateResponse(HttpStatusCode.OK, analysis);
        }
    }
}