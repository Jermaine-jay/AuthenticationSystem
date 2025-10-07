using AuthSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthSystem.Infrastructure.Repositories.EntityConfiguration
{
    class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.HasData(new List<Role>
            {
                new Role{Id= Guid.NewGuid().ToString(), Name="Admin", DateCreated=DateTime.UtcNow,LastModified=DateTime.UtcNow, IsDeleted=false},
                new Role{Id= Guid.NewGuid().ToString(), Name="User", DateCreated=DateTime.UtcNow,LastModified=DateTime.UtcNow, IsDeleted=false},
            });
        }
    }
}
