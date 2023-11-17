using Microsoft.EntityFrameworkCore;

namespace IdentityServer.AuthServer.Models
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions opts):base(opts)
        {
                
        }

        public DbSet<CustomUser> CustomUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomUser>().HasData(
                new CustomUser() { Id = 1,Email= "burcustael@hotmail.com",Password= "password", City="artvin",UserName="burcus"},
                new CustomUser() { Id = 2, Email = "ahmet@outlook.com", Password = "password", City = "Ankara", UserName = "ahmet16" },
                new CustomUser() { Id = 3, Email = "mehmet@outlook.com", Password = "password", City = "Konya", UserName = "mehmet16" }

                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
