using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using SentimentAnalyzer.Domain;

namespace SentimentAnalyzer.DataAccess
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> collection;

        public UserRepository()
        {
            MongoClient client = new MongoClient("mongodb://sentiment_admin:SentimentAnalyzer1@ds151661.mlab.com:51661/sentiment_analyzer_keywords");

            collection = client
                .GetDatabase("sentiment_analyzer_keywords")
                .GetCollection<User>("users");
        }

        public Task AddAsync(User user)
        {
            return collection.InsertOneAsync(user);   
        }

        public void Add(User user)
        {
            collection.InsertOne(user);
        }

        public Task DeleteAsync(User user)
        {
            return collection.DeleteOneAsync(x => x.Id == user.Id);
        }

        public Task<User> FindByIdAsync(string id)
        {
            return collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }


        public IList<User> SearchFor(Expression<Func<User, bool>> predicate)
        {
            var results = collection
                .Find(predicate)
                .ToListAsync()
                .Result;

            return results;
        }

        public IList<User> GetAll()
        {
            var allUsers = collection
                .Find(p => true)
                .ToListAsync()
                .Result;

            return allUsers;
        }

        public User GetById(Guid id)
        {
            var foundUser = collection
                .Find(e => e.Id == id.ToString())
                .ToListAsync()
                .Result
                .SingleOrDefault();

            return foundUser;
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return collection
               .Find(e => e.Email == userName)
               .SingleOrDefaultAsync();
        }
    }
}
