using AuthSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthSystem.Infrastructure.Repositories.EntityConfiguration
{
    internal class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(x => x.Name);
            builder.HasData(new List<Permission>
            {
                new Permission { Name = "GetAllMinisters", Id = 1, IsDeleted = false },
                new Permission { Name = "GetAllUsers", Id = 2, IsDeleted = false },
                new Permission { Name = "GetSingleUser", Id = 3, IsDeleted = false },
                new Permission { Name = "BlockUser", Id = 4, IsDeleted = false },
                new Permission { Name = "CreateRole", Id = 5, IsDeleted = false },
                new Permission { Name = "UsersInRole", Id = 6, IsDeleted = false },
                new Permission { Name = "AddUserToRole", Id = 7, IsDeleted = false },
                new Permission { Name = "ViewAllPermission", Id = 8, IsDeleted = false },
                new Permission { Name = "RemoveUserFromRole", Id = 9, IsDeleted = false },
                new Permission { Name = "ViewAllRoles", Id = 10, IsDeleted = false },
                new Permission { Name = "RemoveRole", Id = 11, IsDeleted = false },
                new Permission { Name = "AddUserPermission", Id = 12, IsDeleted = false },
                new Permission { Name = "RemoveUserPermission", Id = 13, IsDeleted = false },
                new Permission { Name = "UnblockUser", Id = 14, IsDeleted = false },
            });
        }
    }
}


