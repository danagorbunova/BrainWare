using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Entities
{
    [Table(nameof(Product))]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}