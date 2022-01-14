using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Entities
{
    [Table(nameof(OrderProduct))]
    public class OrderProduct
    {
        [Key]
        [Column("order_id", Order = 0)]
        public int OrderId { get; set; }

        [Key]
        [Column("product_id", Order = 1)]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}