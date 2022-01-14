using System.Data.Entity;

namespace Web.Entities
{
    public interface IAppDbContext
    {
        DbSet<OrderProduct> OrderProducts { get; set; }
    }
}
