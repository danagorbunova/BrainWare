using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Web.Entities
{
    [Table(nameof(Order))]
    public class Order
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("company_id")]
        public int CompanyId { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public Company Company { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public decimal OrderTotal => OrderProducts?.Sum(t => t.Quantity * t.Price) ?? 0;
    }
}