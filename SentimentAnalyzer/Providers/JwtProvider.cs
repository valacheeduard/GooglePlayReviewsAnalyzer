using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SentimentAnalyzer.Providers
{
    public class JwtProvider
    {
        private readonly string secretKey = "1DC7E93592B4EF8393EBA367C69E5900F8CA3D91143245CA6FA80E7A1F119352";

        public string CreateNewToken(string userId, string userEmail, string role)
        {
            var tokenSegments = new List<string>();

            var headerObject = new { alg = "HS256", typ = "JWT" };
            var payloadObject = new
            {
                sub = userId,
                name = userEmail,
                role,
                exp = DateTime.Now.AddHours(2).ToString()
            };

            var hashAlgorithm = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));

            byte[] headerBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(headerObject, Formatting.None));
            byte[] payloadBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payloadObject, Formatting.None));

            tokenSegments.Add(Base64UrlEncode(headerBytes));
            tokenSegments.Add(Base64UrlEncode(payloadBytes));

            var stringToSign = string.Join(".", tokenSegments.ToArray());
            var bytesToSign = Encoding.UTF8.GetBytes(stringToSign);

            var signature = hashAlgorithm.ComputeHash(bytesToSign);

            tokenSegments.Add(Base64UrlEncode(signature));

            return string.Join(".", tokenSegments.ToArray());
        }

        public JObject DecodeToken(string token)
        {
            var parts = token.Split('.');
            var header = parts[0];
            var payload = parts[1];
            byte[] crypto = Base64UrlDecode(parts[2]);

            var headerJson = Encoding.UTF8.GetString(Base64UrlDecode(header));
            var headerData = JObject.Parse(headerJson);
            var payloadJson = Encoding.UTF8.GetString(Base64UrlDecode(payload));
            var payloadData = JObject.Parse(payloadJson);


            var bytesToSign = Encoding.UTF8.GetBytes(string.Concat(header, ".", payload));
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);

            var sha256 = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var signature = sha256.ComputeHash(bytesToSign);


            var decodedCrypto = Convert.ToBase64String(crypto);
            var decodedSignature = Convert.ToBase64String(signature);

            if (decodedCrypto != decodedSignature)
            {
                throw new ApplicationException($"Invalid signature. Expected {decodedCrypto} got {decodedSignature}");
            }

            if (payloadData["exp"] != null)
            {
                var tokenDateTime = DateTime.Parse(payloadData["exp"].ToString());
                var currentDateTime = DateTime.Now;

                if (tokenDateTime < currentDateTime)
                {
                    throw new ApplicationException($"Invalid signature. It expired in {tokenDateTime}.");
                }
            }

            return payloadData;
        }

        private static string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0]; // Remove any trailing '='s
            output = output.Replace('+', '-'); // 62nd char of encoding
            output = output.Replace('/', '_'); // 63rd char of encoding
            return output;
        }

        private static byte[] Base64UrlDecode(string input)
        {
            var output = input;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding
            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: output += "=="; break; // Two pad chars
                case 3: output += "="; break; // One pad char
                default: throw new System.Exception("Illegal base64url string!");
            }
            var converted = Convert.FromBase64String(output); // Standard base64 decoder
            return converted;
        }
    }
}
