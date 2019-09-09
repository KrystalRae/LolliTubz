using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCInBuiltFeatures.Models
{
    public class StockOrderModel
    {
        public string FranchiseName { get; set; }
        public List<BuyerApprovedOrderModel> ApprovedOrders { get; set; }
        public int TotalNumberOfLocations { get; set; }
        public int TotalNumberOfProducts { get; set; }
        public float TotalValueOfProducts { get; set; }

        public StockOrderModel(string franchiseName, List<BuyerApprovedOrderModel> approvedOrders, int numOfLocations, int totalProducts, float totalValue)
        {
            FranchiseName = franchiseName;
            ApprovedOrders = approvedOrders;
            TotalNumberOfLocations = numOfLocations;
            TotalNumberOfProducts = totalProducts;
            TotalValueOfProducts = totalValue;
        }
    }
}