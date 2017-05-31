using System.Web.Mvc;
using SentimentAnalyzer.Business;
using SentimentAnalyzer.Contracts;

namespace SentimentAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private IAppReviewsAppService appReviewsLogic;

        public HomeController()
        {

            //var sentimentAnalysisLogic = new SentimentAnalysisLogic();

            //sentimentAnalysisLogic.AnalyzeReviews("com.facebook.katana");

            //appReviewsLogic = new ReviewsAppService();

            //var keywordsRepo = new KeywordsAppService();

            //var a = appReviewsLogic.GetReviews("com.facebook.katana");
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
