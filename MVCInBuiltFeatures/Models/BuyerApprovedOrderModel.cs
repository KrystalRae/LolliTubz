using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCInBuiltFeatures.Models
{
    public class BuyerApprovedOrderModel
    {
        public string LocationName { get; set; }
        public int TotalProductQuantity { get; set; }
        public int OrderValue { get; set; }
        public DateTime OrderDate { get; set; }

        public BuyerApprovedOrderModel(string locationName, int totalProductQuantity, int orderValue, DateTime orderDate)
        {
            LocationName = locationName;
            TotalProductQuantity = totalProductQuantity;
            OrderValue = orderValue;
            OrderDate = orderDate;
        }
    }
}