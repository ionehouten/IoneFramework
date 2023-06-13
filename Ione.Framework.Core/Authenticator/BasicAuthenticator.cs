using System;
using System.Text;

namespace Ione.Framework.Core.Authenticator
{
    public class BasicAuthenticator : IAuthenticator
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public BasicAuthenticator()
        {

        }
        public BasicAuthenticator(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public static string Encode(BasicAuthenticator input)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", input.Username, input.Password));

            return Convert.ToBase64String(byteData);
        }

        public static BasicAuthenticator Decode(string encode)
        {
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string byteString = encoding.GetString(Convert.FromBase64String(encode));

            int seperatorIndex = byteString.IndexOf(':');

            return new BasicAuthenticator()
            {
                Username = byteString.Substring(0, seperatorIndex),
                Password = byteString.Substring(seperatorIndex + 1)
            };
            
        }
    }
}
