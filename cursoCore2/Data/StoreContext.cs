using cursoCore2API.Models;
using Microsoft.EntityFrameworkCore;

namespace cursoCore2API.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Categoria> categoria { get; set; }
        public DbSet<Producto> productos { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Marcas> marcas { get; set; }

    }
}
