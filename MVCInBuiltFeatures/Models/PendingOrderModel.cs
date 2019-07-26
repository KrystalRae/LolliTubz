using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCInBuiltFeatures.Models
{
    public class PendingOrderModel
    {
        public int OrderId { get; set; }
        public string LocationName { get; set; }
        public int TotalNumberOfProducts { get; set; }
        public DateTime OrderDate { get; set; }

        public PendingOrderModel(int orderId, string locationName, int totalNumProducts, DateTime orderDate)
        {
            OrderId = orderId;
            LocationName = locationName;
            TotalNumberOfProducts = totalNumProducts;
            OrderDate = orderDate;
        }
    }
}