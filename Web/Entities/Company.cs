using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Entities
{
    [Table(nameof(Company))]
    public class Company
    {
        [Key]
        [Column("company_id")]
        public int CompanyId { get; set; }

        public string Name { get; set; }
    }
}