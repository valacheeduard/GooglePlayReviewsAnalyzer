using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using SentimentAnalyzer.Business;
using SentimentAnalyzer.Contracts;
using SentimentAnalyzer.Models;
using SentimentAnalyzer.Providers;

namespace SentimentAnalyzer.Controllers
{
    public class TokenController : ApiController
    {
        private readonly JwtProvider jwtProvider;

        private readonly IUserAppService userAppService;

        public TokenController()
        {
            jwtProvider = new JwtProvider();
            userAppService = new UserAppService();
        }

        [HttpPost]
        [Route("api/token")]
        public HttpResponseMessage CreateToken(LoginUserModel user)
        {
            var foundUser = userAppService.GetUserByEmailAndPassword(user.Email, user.Password);

            if (foundUser != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new { token = jwtProvider.CreateNewToken(foundUser.Id, foundUser.Email, "admin") });
            }

            return Request.CreateResponse(HttpStatusCode.Forbidden);
        }
    }
}