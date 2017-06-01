﻿using System;
using System.Collections.Generic;
using SentimentAnalyzer.Contracts;
using SentimentAnalyzer.DataAccess;
using SentimentAnalyzer.Domain;

namespace SentimentAnalyzer.Business
{
    public class KeywordsAppService : IKeywordsAppService
    {
        private readonly KeywordRepository repository;

        public KeywordsAppService()
        {
            repository = new KeywordRepository();
        } 

        public Keyword AddKeyword(string keyword, string categoryCode)
        {
            var newKeyword = new Keyword
            {
                Id = Guid.NewGuid().ToString(),
                Content = keyword.ToLower()
            };

            switch (categoryCode)
            {
                case "EXP":
                    newKeyword.Category = "EXP";
                    newKeyword.Weight = KeywordCategories.Categories["EXP"];
                    break;

                case "P":
                    newKeyword.Category = "P";
                    newKeyword.Weight = KeywordCategories.Categories["P"];
                    break;

                case "NEUT":
                    newKeyword.Category = "NEUT";
                    newKeyword.Weight = KeywordCategories.Categories["NEUT"];
                    break;

                case "N":
                    newKeyword.Category = "N";
                    newKeyword.Weight = KeywordCategories.Categories["N"];
                    break;

                case "EXN":
                    newKeyword.Category = "EXN";
                    newKeyword.Weight = KeywordCategories.Categories["EXN"];
                    break;
            }

            repository.Add(newKeyword);

            return newKeyword;
        }

        public IEnumerable<Keyword> GetAll()
        {
            return repository.GetAll();
        }

        public void Delete(string id)
        {
            repository.Remove(id);
        }
    }
}
