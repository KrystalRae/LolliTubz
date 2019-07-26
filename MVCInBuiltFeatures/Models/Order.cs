using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCInBuiltFeatures.Models
{
    public enum OrderStatusEnum
    {
        NotApproved = 1,
        BuyerApproved = 2
    }

    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public int LocationId { get; set; }
        
        [ForeignKey("OrderStatusId")]
        public virtual OrderStatus OrderStatus { get; set; }
        public int OrderStatusId { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }

        public DateTime OrderDate { get; set; }

        public Order(int locationId, List<OrderLine> orderLines, OrderStatusEnum orderStatus)
        {
            LocationId = locationId;
            OrderLines = orderLines;
            OrderStatusId = (int)orderStatus;
            OrderDate = DateTime.Today;
        }

        public Order()
        {
            OrderDate = DateTime.Today;
        }
    }

    [Table("OrderStatus")]
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public string Name { get; set; }
    }
}