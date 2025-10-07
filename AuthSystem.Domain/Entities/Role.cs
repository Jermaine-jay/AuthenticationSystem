namespace AuthSystem.Domain.Entities
{
    public class Role : BaseEntity<string>
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DateDeleted { get; set; }
    }
}
