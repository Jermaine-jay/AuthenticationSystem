using AuthSystem.Domain.Entities;
using AuthSystem.Infrastructure.Repositories.EntityConfiguration;
using Microsoft.EntityFrameworkCore;


namespace AuthSystem.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<UserPermission> UserPermission { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration<Role>(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration<Permission>(new PermissionEntityConfiguration());
            modelBuilder.ApplyConfiguration<UserInRole>(new UserInRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration<RolePermission>(new RolePermissionEntityConfiguration());
            modelBuilder.ApplyConfiguration<UserPermission>(new UserPermissionEntityConfiguration());
        }
    }
}
