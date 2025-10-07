using AuthSystem.Domain.Enum;

namespace AuthSystem.Domain.Entities
{
    public class User : BaseEntity<string>
    {
        public string Email { get; set; }
        public string? Salt { get; set; }
        public bool IsBanned { get; set; }
        public bool IsDeleted { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? Password { get; set; }
        public string PhoneNumber { get; set; }
        public string? MiddleName { get; set; }
        public UserType UserTypeId { get; set; }
        public int LoginFailedCount { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
