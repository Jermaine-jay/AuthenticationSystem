using AuthSystem.Domain.Enum;

namespace AuthSystem.Application.Dtos.Request
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public bool IsBanned { get; set; }
        public string PhoneNumber { get; set; }
        public UserType UserTypeId { get; set; }
        public int LoginFailedCount { get; set; }
    }
}
