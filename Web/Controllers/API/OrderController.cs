using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Web.Entities;

namespace Web.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IAppDbContext _appDbContext;

        public OrderController(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IEnumerable<Order> GetOrders(int id = 1)
        {
            var orders = _appDbContext
                .OrderProducts
                .Where(t => t.Order.CompanyId == id)
                .Include(t => t.Product)
                .Include(t => t.Order)
                .ToArray()
                .Select(t => t.Order)
                .Distinct()
                .ToArray();

            return orders;
        }
    }
}
