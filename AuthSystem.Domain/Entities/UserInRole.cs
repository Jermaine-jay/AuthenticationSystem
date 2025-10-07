namespace AuthSystem.Domain.Entities
{
    public class UserInRole : BaseEntity<string>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
