namespace AuthSystem.Application.Helpers
{
    public class JwtToken
    {
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public DateTime? Issued { get; set; }
        public DateTime? Expires { get; set; }
        public string Email { get; set; } = null!;
        public string[] Permissions { get; set; } = null!;
    }
}
