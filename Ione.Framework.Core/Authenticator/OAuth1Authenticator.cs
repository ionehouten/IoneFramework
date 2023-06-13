using System;

namespace Ione.Framework.Core.Authenticator
{
    public class OAuth1Authenticator : IAuthenticator
    {
        public Uri Url { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string TokenSecret { get; set; }
        public string Timestamp { get; set; }
        public string Nonce { get; set; }
        public string Version { get; set; }
        public string Realm { get; set; }
        public string Signature { get; set; }
        public string Method { get; set; }
        public OAuth.SignatureTypes SignatureMethod { get; set; }

        public static OAuth1Authenticator ForAccessToken(string consumerKey, string consumerSecret, string token, string tokenSecret)
        {
            return new OAuth1Authenticator()
            {
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                AccessToken = token,
                TokenSecret = tokenSecret
            };
        }
    }
}
