﻿namespace SentimentAnalyzer.Domain
{
    public class User
    {
        public string Id { get; set; }

        public string UserName { get; set; }


        public string Email { get; set; }

        public string HashedPassword { get; set; }
    }
}
