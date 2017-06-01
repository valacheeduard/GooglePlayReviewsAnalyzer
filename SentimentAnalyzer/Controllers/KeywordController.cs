using System.Net;
using System.Net.Http;
using System.Web.Http;
using SentimentAnalyzer.Business;
using SentimentAnalyzer.Business.DTOs;
using SentimentAnalyzer.Contracts;
using SentimentAnalyzer.Providers;

namespace SentimentAnalyzer.Controllers
{
    [TokenAuthorization]
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
            var addedKeyword = appService.AddKeyword(keyword.Keyword, keyword.Category);

            return Request.CreateResponse(HttpStatusCode.OK, addedKeyword);
        }

        [HttpGet]
        [Route("api/keyword")]
        public HttpResponseMessage Get()
        {
            var allKeywords = appService.GetAll();

            return Request.CreateResponse(HttpStatusCode.OK, allKeywords);
        }

        [HttpDelete]
        [Route("api/keyword/{id}")]
        public HttpResponseMessage Delete(string id)
        {
            appService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}