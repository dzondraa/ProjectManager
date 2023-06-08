namespace Implementation
{
    public class AppSettings
    {
        public JwtSettings Jwt { get; set; }
        
        public string BugSnagKey { get; set; }

        public ConnectionStrings connectionStrings { get; set; }

        public string FileServerRoot { get; set; }
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int DurationSeconds { get; set; }
        public string Issuer { get; set; }
    }

    public class ConnectionStrings
    {
        public string DB { get; set; }
    }
}
