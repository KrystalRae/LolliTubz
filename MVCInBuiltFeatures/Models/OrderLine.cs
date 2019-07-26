using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCInBuiltFeatures.Models
{
    public class OrderLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderLineId { get; set; }

        [ForeignKey("ProductCode")]
        public virtual Product Product { get; set; }
        public string ProductCode { get; set; }
        
        public int Quantity { get; set; }

        public Order Order { get; set; }

        public OrderLine (string productCode, int quantity)
        {
            ProductCode = productCode;
            Quantity = quantity;
        }

        public OrderLine()
        {

        }
    }
}