using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Newtonsoft.Json.Linq;

namespace SentimentAnalyzer.Providers
{
    public class TokenAuthorizationAttribute  : Attribute, IAuthenticationFilter
    {
        private readonly JwtProvider jwtProvider;

        public TokenAuthorizationAttribute()
        {
            jwtProvider = new JwtProvider();
        }

        public bool AllowMultiple { get; }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (context.Request.Headers.Authorization == null)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return Task.FromResult(0);
            }

            var tokenSource = context.Request.Headers.Authorization.ToString().Split(' ')[1];

            if (tokenSource == null)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return Task.FromResult(0);
            }

            JObject result = new JObject();

            try
            {
                result = jwtProvider.DecodeToken(tokenSource);
            }
            catch (Exception e)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return Task.FromResult(0);
            }

            if (result == null)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return Task.FromResult(0);
            }

            var expiration = DateTime.Parse(result["exp"].ToString());

            if (expiration < DateTime.Now)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return Task.FromResult(0);
            }

            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}