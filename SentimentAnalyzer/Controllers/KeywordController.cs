using System.Net;
using System.Net.Http;
using System.Web.Http;
using SentimentAnalyzer.Business;
using SentimentAnalyzer.Business.DTOs;
using SentimentAnalyzer.Contracts;

namespace SentimentAnalyzer.Controllers
{
    public class KeywordController : ApiController
    {
        private readonly IKeywordsAppService appService;

        public KeywordController()
        {
            appService = new KeywordsAppService();
        }

        [HttpPost]
        [Route("api/keyword")]
        public HttpResponseMessage AddKeyword(KeywordDto keyword)
        {
            appService.AddKeyword(keyword.Keyword, keyword.Category);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}