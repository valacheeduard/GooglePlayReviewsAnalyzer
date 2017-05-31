using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using SentimentAnalyzer.Domain;

namespace SentimentAnalyzer.DataAccess
{
    public class KeywordRepository {

        private readonly IMongoCollection<Keyword> collection;

        public KeywordRepository()
        {
            MongoClient client = new MongoClient("mongodb://sentiment_admin:SentimentAnalyzer1@ds151661.mlab.com:51661/sentiment_analyzer_keywords");

            collection = client
                .GetDatabase("sentiment_analyzer_keywords")
                .GetCollection<Keyword>("keywords");
        }

        public void Add(Keyword keyword)
        {
            collection.InsertOne(keyword);
        }

        public IList<Keyword> SearchFor(Expression<Func<Keyword, bool>> predicate)
        {
            var results = collection
                .Find(predicate)
                .ToListAsync()
                .Result;

            return results;
        }

        public IList<Keyword> GetAll()
        {
            var allEmployees = collection
                .Find(p => true)
                .ToListAsync()
                .Result;

            return allEmployees;
        }

        public Keyword GetById(Guid id)
        {
            var foundEmployee = collection
                .Find(e => e.Id == id.ToString())
                .ToListAsync()
                .Result
                .SingleOrDefault();

            return foundEmployee;
        }
    }
}
