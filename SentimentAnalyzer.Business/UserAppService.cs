using System.Linq;
using SentimentAnalyzer.Contracts;
using SentimentAnalyzer.DataAccess;
using SentimentAnalyzer.Domain;

namespace SentimentAnalyzer.Business
{
    public class UserAppService: IUserAppService
    {
        private readonly UserRepository userRepository;

        public UserAppService()
        {
            userRepository = new UserRepository();
        }

        public User GetUserByEmailAndPassword(string username, string password)
        {
            var hashedPassword = PasswordHasher.GetHash(password);

            return userRepository
                .SearchFor(x => x.UserName == username && x.HashedPassword == hashedPassword)
                .SingleOrDefault();
        }
    }
}
