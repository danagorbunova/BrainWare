using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Entities;

namespace Tests.Controllers.API
{
    [TestClass]
    public class OrderControllerTest
    {
        private class StubDbContext : IAppDbContext
        {
            private readonly IQueryable<OrderProduct> _data;

            public IQueryable<OrderProduct> OrderProducts => _data;

            public StubDbContext()
            {
                var products = new[]
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Some product",
                        Price = 99
                    },
                    new Product()
                    {
                        ProductId = 2,
                        Name = "Another product",
                        Price = 10
                    }
                };

                var orders = new[]
                {
                    new Order()
                    {
                        OrderId = 1,
                        Description = "Some order",
                        CompanyId = 1,
                        OrderProducts = new[]
                        {
                            new OrderProduct()
                            {
                                Product = products[0],
                                Quantity = 10,
                                Price = 256
                            }
                        }
                    },
                    new Order()
                    {
                        OrderId = 2,
                        Description = "Another order",
                        CompanyId = 2,
                        OrderProducts = new[]
                        {
                            new OrderProduct()
                            {
                                Product = products[0],
                                Quantity = 2,
                                Price = 8
                            },
                            new OrderProduct()
                            {
                                Product = products[1],
                                Quantity = 5,
                                Price = 10
                            }
                        }
                    },
                    new Order()
                    {
                        OrderId = 3,
                        Description = "Yet another order",
                        CompanyId = 2,
                        OrderProducts = new[]
                        {
                            new OrderProduct()
                            {
                                Product = products[0],
                                Quantity = 2,
                                Price = 8
                            },
                            new OrderProduct()
                            {
                                Product = products[1],
                                Quantity = 2,
                                Price = 8
                            }
                        }
                    }
                };

                foreach (var order in orders)
                {
                    foreach (var orderProduct in order.OrderProducts)
                    {
                        orderProduct.Order = order;
                    }
                }

                _data = orders.SelectMany(t => t.OrderProducts).AsQueryable();
            }
        }

        [TestMethod]
        public void GetOrders()
        {
            var dbContext = new StubDbContext();
            OrderController controller = new OrderController(dbContext);

            var result = controller.GetOrders(2);

            var expectedOrders = dbContext
                .OrderProducts
                .Where(t => t.Order.CompanyId == 2)
                .Select(t => t.Order)
                .Distinct()
                .ToArray();

            Assert.AreEqual(expectedOrders.Count(), result.Count());

            foreach (var expectedOrder in expectedOrders)
            {
                var actualOrder = result.Single(t => t.OrderId == expectedOrder.OrderId);

                Assert.AreEqual(expectedOrder, actualOrder);
            }
        }
    }
}
