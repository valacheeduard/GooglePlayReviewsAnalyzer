using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                .Select(x => x.InnerText.Replace("Full Review", ""));

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

        public string DownloadAnalysis(string appId)
        {
            string url = "https://play.google.com/store/apps/details?id=" + appId + "&hl=en";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);

            var allReviewsContent = document
                .GetDivsByClass("review-body with-review-wrapper")
                .Select(x => x.InnerText.Replace("Full Review", "").Replace(", ", " "));

            var comments = new List<SentimentCommentAnalysisDocument>();

            var appName = document.GetDivsByClass("id-app-title").First().InnerText;

            foreach (var reviewContent in allReviewsContent)
            {
                var newComment = new SentimentCommentAnalysisDocument();
                newComment.Keywords = new List<Keyword>();
                newComment.Text = reviewContent;

                var splittedWords = reviewContent.Split(' ').Select(FormatWord);
                var existingKeywords = keywordRepository.GetAll();

                foreach (var keyword in existingKeywords)
                {
                    var foundKeywords = splittedWords.Where(x => x == keyword.Content).ToList();

                    foreach (var found in foundKeywords)
                    {
                        newComment.Keywords.Add(keyword);
                    }
                }


                comments.Add(newComment);
            }

            var finalTextDocument = new StringBuilder();

            finalTextDocument.AppendLine("Reviews for: " + appName );

            finalTextDocument.Append("Review content:");
            finalTextDocument.Append(",");
            finalTextDocument.Append("Extreme positive words:");
            finalTextDocument.Append(",");
            finalTextDocument.Append("Positive words:");
            finalTextDocument.Append(",");
            finalTextDocument.Append("Neutral words:");
            finalTextDocument.Append(",");
            finalTextDocument.Append("Negative words:");
            finalTextDocument.Append(",");
            finalTextDocument.Append("Extreme negative words:");
            finalTextDocument.Append(",");
            finalTextDocument.Append("Extreme positive weight:");
            finalTextDocument.Append(",");
            finalTextDocument.Append("Positive weight:");
            finalTextDocument.Append(",");
            finalTextDocument.Append("Neutral weight:");
            finalTextDocument.Append(",");
            finalTextDocument.Append("Negative weight:");
            finalTextDocument.Append(",");
            finalTextDocument.Append("Extreme negative weight:");
            finalTextDocument.AppendLine();

            foreach (var comment in comments)
            {
                finalTextDocument.Append(comment.Text);
                finalTextDocument.Append(",");
                finalTextDocument.Append(string.Join(" ", comment.Keywords.Where(x => x.Category == "EXP").Select(x => x.Content)));
                finalTextDocument.Append(",");
                finalTextDocument.Append(string.Join(" ", comment.Keywords.Where(x => x.Category == "P").Select(x => x.Content)));
                finalTextDocument.Append(",");
                finalTextDocument.Append(string.Join(" ", comment.Keywords.Where(x => x.Category == "NEUT").Select(x => x.Content)));
                finalTextDocument.Append(",");
                finalTextDocument.Append(string.Join(" ", comment.Keywords.Where(x => x.Category == "N").Select(x => x.Content)));
                finalTextDocument.Append(",");
                finalTextDocument.Append(string.Join(" ", comment.Keywords.Where(x => x.Category == "EXN").Select(x => x.Content)));

                finalTextDocument.Append(",");
                finalTextDocument.Append(comment.Keywords.Where(x => x.Category == "EXP").Select(x => x.Weight).Sum());
                finalTextDocument.Append(",");
                finalTextDocument.Append(comment.Keywords.Where(x => x.Category == "P").Select(x => x.Weight).Sum());
                finalTextDocument.Append(",");
                finalTextDocument.Append(comment.Keywords.Where(x => x.Category == "NEUT").Select(x => x.Weight).Sum());
                finalTextDocument.Append(",");
                finalTextDocument.Append(comment.Keywords.Where(x => x.Category == "N").Select(x => x.Weight).Sum());
                finalTextDocument.Append(",");
                finalTextDocument.Append(comment.Keywords.Where(x => x.Category == "EXN").Select(x => x.Weight).Sum());

                finalTextDocument.AppendLine();
            }

            return finalTextDocument.ToString();
        }

        public string FormatWord(string word)
        {
            var newWord = word.ToLower();

            newWord = newWord.Replace(".", "");
            newWord = newWord.Replace(",", "");
            newWord = newWord.Replace("!", "");
            newWord = newWord.Replace("?", "");
            newWord = newWord.Replace(" ", "");

            return newWord;
        }
    }
}
