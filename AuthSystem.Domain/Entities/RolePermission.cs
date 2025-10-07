namespace AuthSystem.Domain.Entities
{
    public partial class RolePermission : BaseEntity<string>
    {
        public string RoleId { get; set; }
        public int PermissionId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
