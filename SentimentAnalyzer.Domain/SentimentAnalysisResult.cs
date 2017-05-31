using System.Collections.Generic;
using System.Linq;

namespace SentimentAnalyzer.Domain
{
    public class SentimentAnalysisResult
    {
        public SentimentAnalysisResult()
        {
            ExtremePositiveWords = new List<Keyword>();
            PositiveWords = new List<Keyword>();
            NeutralWords = new List<Keyword>();
            NegativeWords = new List<Keyword>();
            ExtremeNegativeWords = new List<Keyword>();
        }

        public List<Keyword> ExtremePositiveWords { get; set; }

        public List<Keyword> PositiveWords { get; set; }
        
        public List<Keyword> NeutralWords { get; set; }
        
        public List<Keyword> NegativeWords { get; set; }
        
        public List<Keyword> ExtremeNegativeWords { get; set; }

        public void AddKeyword(Keyword word)
        {
            switch (word.Category)
            {
                case "EXP":
                    ExtremePositiveWords.Add(word);
                    break;

                case "P":
                    PositiveWords.Add(word);

                    break;

                case "NEUT":
                    NeutralWords.Add(word);
                    break;

                case "N":
                    NegativeWords.Add(word);
                    break;

                case "EXN":
                    ExtremeNegativeWords.Add(word);
                    break;
            }
        }

        public int ExtremePositiveWeight
        {
            get { return ExtremePositiveWords.Sum(x => x.Weight); }
        }

        public int PositiveWeight
        {
            get { return PositiveWords.Sum(x => x.Weight); }
        }

        public int NeutralWeight
        {
            get { return NeutralWords.Sum(x => x.Weight); }
        }

        public int NegativeWeight
        {
            get { return NegativeWords.Sum(x => x.Weight); }
        }

        public int ExtremeNegativeWeight
        {
            get { return ExtremeNegativeWords.Sum(x => x.Weight); }
        }

        public int TotalWeight
        {
            get
            {
                var total = 0;

                total += ExtremePositiveWords.Sum(word => word.Weight);
                total += PositiveWords.Sum(word => word.Weight);
                total += NeutralWords.Sum(word => word.Weight);
                total += NegativeWords.Sum(word => word.Weight);
                total += ExtremeNegativeWords.Sum(word => word.Weight);

                return total;
            }
        }
    }
}
