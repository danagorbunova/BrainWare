using System.Linq;

namespace Web.Entities
{
    public interface IAppDbContext
    {
        IQueryable<OrderProduct> OrderProducts { get; }
    }
}
