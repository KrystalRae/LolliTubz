using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCInBuiltFeatures.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        [Display(Name = "Name")]
        public string ProductName { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Cost")]
        public float Cost { get; set; }

        public Product()
        {

        }

        public Product(string name, string code, float cost)
        {
            ProductName = name;
            ProductCode = code;
            Cost = cost;
            Quantity = 0;
        }

    }
}