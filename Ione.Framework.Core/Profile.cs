namespace Ione.Framework.Core
{
    /// <summary>
    /// Profile 
    /// untuk pengaturan akses database
    /// </summary>
    public class Profile
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public uint Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
