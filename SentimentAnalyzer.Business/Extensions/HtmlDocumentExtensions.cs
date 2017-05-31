using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace SentimentAnalyzer.Business.Extensions
{
    public static class HtmlDocumentExtensions
    {
        public static IEnumerable<HtmlNode> GetDivsByClass(this HtmlDocument document, string className)
        {
            return document.DocumentNode.Descendants("div")
                .Where(d => d.Attributes.Contains("class")
                            && d.Attributes["class"].Value.Contains(className))
                .ToList();
        }

        public static IEnumerable<HtmlNode> GetSpansByClass(this HtmlDocument document, string className)
        {
            return document.DocumentNode.Descendants("span")
                .Where(d => d.Attributes.Contains("class")
                            && d.Attributes["class"].Value.Contains(className))
                .ToList();
        }
    }
}
