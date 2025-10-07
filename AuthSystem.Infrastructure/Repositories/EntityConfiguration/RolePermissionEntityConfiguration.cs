using AuthSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthSystem.Infrastructure.Repositories.EntityConfiguration
{
    internal class RolePermissionEntityConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.PermissionId).IsRequired();
            builder.Property(x => x.RoleId).IsRequired();
            builder.HasData(new List<RolePermission>
            {
                // Admin Permission Mapping
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 1,  DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 2,  DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 3,  DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 4,  DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 5,  DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 6,  DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 7,  DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 8,  DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 9,  DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 10, DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 11, DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 12, DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 13, DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
                new RolePermission { Id = Guid.NewGuid().ToString(), RoleId = "6ec0c60b-1219-4417-ab71-ab1e354f9082", PermissionId = 14, DateCreated = DateTime.UtcNow, LastModified = DateTime.UtcNow },
            });
        }
    }
}
