using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        public HttpResponseMessage GetAnalysisResult(string appId)
        {
            var result = appService.AnalyzeReviews(appId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/sentiment/download")]
        public HttpResponseMessage DownloadAnalysis(string appId)
        {
            var csvStream = appService.DownloadAnalysis(appId);

            var result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(csvStream)));
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
    }
}