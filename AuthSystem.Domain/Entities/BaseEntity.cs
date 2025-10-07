namespace AuthSystem.Domain.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime LastModified { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
    }
}
