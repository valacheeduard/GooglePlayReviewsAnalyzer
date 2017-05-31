﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace SentimentAnalyzer.Providers
{
    public class PasswordHasher
    {
        public string GetHash(string source)
        {
            SHA256 mySHA256 = SHA256.Create();

            var byteSource = Encoding.ASCII.GetBytes(source);

            var hashedValue = mySHA256.ComputeHash(byteSource);

            return Convert.ToBase64String(hashedValue);
        }
    }
}