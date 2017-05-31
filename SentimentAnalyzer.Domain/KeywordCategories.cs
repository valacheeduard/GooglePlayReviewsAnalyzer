using System.Collections.Generic;

namespace SentimentAnalyzer.Domain
{
    public static class KeywordCategories
    {
        public static Dictionary<string, int> Categories = new Dictionary<string, int>
        {     
            { "EXP", 10 },
            { "P", 5 },
            { "NEUT", 0 },
            { "N", -5 },
            { "EXN", -10 }
        }; 
    }
}
