namespace Ione.Framework.Core.Authenticator
{
    public class JwtAuthenticator : IAuthenticator
    {
        public string AccessToken { get; set; }
        public JwtAuthenticator()
        {

        }
        public JwtAuthenticator(string accessToken)
        {
            this.AccessToken = accessToken;
        }
    }
}
