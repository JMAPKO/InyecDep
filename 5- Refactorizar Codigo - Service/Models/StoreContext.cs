using Microsoft.EntityFrameworkCore;

namespace BACKEND02.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {}

        public DbSet<Auto> Autos { get; set; }
        public DbSet<Marca> Marcas { get; set; }

    }
}
