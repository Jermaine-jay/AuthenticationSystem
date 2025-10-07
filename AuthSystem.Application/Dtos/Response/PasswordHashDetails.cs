namespace AuthSystem.Application.Dtos.Response
{
    public class PasswordHashDetails
    {
        public string Salt { get; set; }
        public string HashedValue { get; set; }
    }
}
