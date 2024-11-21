using cursoCore2.Models;
using Microsoft.EntityFrameworkCore;

namespace cursoCore2API.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base (options) 
        { }

        public DbSet<Producto> productos { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Marcas> marcas { get; set; }   

    }
}
