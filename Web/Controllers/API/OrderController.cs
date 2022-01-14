using System.Collections.Generic;
using System.Web.Http;
using Web.Infrastructure;
using Web.Models;

namespace Web.Controllers
{
    public class OrderController : ApiController
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<Order> GetOrders(int id = 1)
        {
            return _orderService.GetOrdersForCompany(id);
        }
    }
}
