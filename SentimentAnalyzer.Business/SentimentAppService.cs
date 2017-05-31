using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using SentimentAnalyzer.Business.Extensions;
using SentimentAnalyzer.Contracts;
using SentimentAnalyzer.DataAccess;
using SentimentAnalyzer.Domain;

namespace SentimentAnalyzer.Business
{
    public class SentimentAppService: ISentimentAppService
    {
        private readonly KeywordRepository keywordRepository;

        public SentimentAppService()
        {
            keywordRepository = new KeywordRepository();
        }

        public SentimentAnalysisResult AnalyzeReviews(string appId)
        {
            string url = "https://play.google.com/store/apps/details?id=" + appId + "&hl=en";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);

            var allReviewsContent = document
                .GetDivsByClass("review-body with-review-wrapper")
                .Select(x => x.InnerText);

            var analysisResult = new SentimentAnalysisResult();

            var allWords = new List<string>();

            foreach (var words in allReviewsContent.Select(review => review.Split(' ')))
            {
                allWords.AddRange(words.Select(FormatWord));
            }

            var existingKeywords = keywordRepository.GetAll();

            foreach (var keyword in existingKeywords)
            {
                var foundKeywords = allWords.Where(x => x == keyword.Content).ToList();

                foreach(var found in foundKeywords)
                {
                   analysisResult.AddKeyword(keyword);
                }
            }

            return analysisResult;
        }

        public string FormatWord(string word)
        {
            var newWord = word.ToLower();

            newWord = newWord.Replace(".", "");
            newWord = newWord.Replace(",", "");
            newWord = newWord.Replace("!", "");
            newWord = newWord.Replace("?", "");

            return newWord;
        }
    }
}
