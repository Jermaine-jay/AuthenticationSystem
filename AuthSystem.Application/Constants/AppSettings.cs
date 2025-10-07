namespace AuthSystem.Application.Constants
{
    public class AppSettings
    {
        public string AppURL { get; set; }
        public int UserRoleId { get; set; }
        public int LockOutTime { get; set; }
        public string AppEmail { get; set; }
        public int LoginFailedAttempt { get; set; }
        public bool IsTestEnvironment { get; set; }
        public string? PepperKey { get; set; }
        public int PasswordHashIteration { get; set; }

    }
}
