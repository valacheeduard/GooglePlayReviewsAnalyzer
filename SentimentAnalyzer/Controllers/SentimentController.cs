using System.Web.Http;
using SentimentAnalyzer.Business;
using SentimentAnalyzer.Contracts;
using SentimentAnalyzer.Domain;

namespace SentimentAnalyzer.Controllers
{
    public class SentimentController : ApiController
    {
        private readonly ISentimentAppService appService;

        public SentimentController()
        {
            appService = new SentimentAppService();
        }

        [HttpGet]
        [Route("api/sentiment")]
        public SentimentAnalysisResult GetAnalysisResult(string appId)
        {
            return appService.AnalyzeReviews(appId);
        }
    }
}