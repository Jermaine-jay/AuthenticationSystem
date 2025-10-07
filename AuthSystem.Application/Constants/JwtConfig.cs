namespace AuthSystem.Application.Constants
{
    public class JwtConfig
    {
        public string? UserSecret { get; set; }
        public string? AdminSecret { get; set; }
        public double Expires { get; set; }
        public string? ImpersonationExpires { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int TokenTimeout { get; set; }
    }
}
