using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCInBuiltFeatures.Models
{
    public class OrderSummaryModel
    {
        public Location Location { get; set; }
        public List<Product> OrderProductList { get; set; }
        public int OrderId { get; set; }
        public int TotalNumberOfProducts { get; set; }

        public OrderSummaryModel()
        {

        }

        public OrderSummaryModel(Location location, List<Product> orderProductList, int orderId, int totalNumProducts)
        {
            Location = location;
            OrderProductList = orderProductList;
            OrderId = orderId;
            TotalNumberOfProducts = totalNumProducts;
        }

    }
}