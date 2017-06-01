using System.Collections.Generic;

namespace SentimentAnalyzer.Domain
{
    public class SentimentCommentAnalysisDocument
    {
        public string Text { get; set; }

        public int Rating { get; set; }

        public List<Keyword> Keywords { get; set; }
    }
}
