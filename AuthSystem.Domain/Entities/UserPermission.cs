namespace AuthSystem.Domain.Entities
{
    public class UserPermission : BaseEntity<string>
    {
        public string UserId { get; set; }
        public int PermissionId { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }
        public DateTime? DateDeleted { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual User User { get; set; }
    }
}
