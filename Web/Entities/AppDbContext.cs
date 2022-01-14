using System.Data.Entity;
using System.Diagnostics;

namespace Web.Entities
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext()
        {
#if DEBUG
            Database.Log = delegate (string text) { Trace.Write(text); };
#endif
        }
    }
}
