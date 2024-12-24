using cursoCore2API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cursoCore2API.Data
{
    public class StoreContext : IdentityDbContext<AppUser>
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Categoria> categoria { get; set; }
        public DbSet<Producto> productos { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<AppUser> AppUser { get; set; } 
        public DbSet<Marcas> marcas { get; set; }

    }
}
