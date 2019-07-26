using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCInBuiltFeatures.Models
{
    public class StockFillModel
    {
        public Location Location { get; set; }
        public List<Product> Stock { get; set; }

        public StockFillModel()
        { }

        public StockFillModel(Location location, List<Product> stock)
        {
            Location = location;
            Stock = stock;
        }
    }
}