using Microsoft.EntityFrameworkCore;
using SecureApiWithAuthentication.Authorization;

namespace SecureApiWithAuthentication.Data
{
    public class AppDbcontext: DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options):base(options)
        {
            
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<users> Users { get; set; }
        public DbSet<UserPermissions> Permissions { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserPermissions>().HasKey(x => new { x.userId, x.PermissionId });
            modelBuilder.Entity<UserPermissions>().ToTable("permissions");
            

            modelBuilder.Entity<UserPermissions>().ToTable("userPermissions");
            modelBuilder.Entity<users>().HasKey(x => x.Id);
            modelBuilder.Entity<users>().Property(x => x.Name).HasColumnType("nvarchar(255)");
            modelBuilder.Entity<users>().Property(x => x.Email).HasColumnType("nvarchar(255)");
            modelBuilder.Entity<users>().Property(x => x.Password).HasColumnType("nvarchar(255)");
            modelBuilder.Entity<users>().ToTable("Users");
             


        }


    }
}
