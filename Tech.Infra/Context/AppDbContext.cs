using Microsoft.EntityFrameworkCore;
using Tech.Domain.Entities;
using Tech.Domain.Enums;

namespace Tech.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>().HasData(
                new Users(
                    name: "admin",
                    password: "123",
                    email: "admin@tech.com",
                    permission: TypePermissionEnum.Admin
                )
                {
                    Id = 1
                },
                new Users(
                    name: "joao",
                    password: "123",
                    email: "joao@tech.com",
                    permission: TypePermissionEnum.User
                )
                {
                    Id = 2
                }
            );
        }

    }


}
