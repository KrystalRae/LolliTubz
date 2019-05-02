using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCInBuiltFeatures.Models
{
    public class AddLocationViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Client")]
        public int SelectedClientId { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }

        [Required]
        [Display(Name = "Charity")]
        public int SelectedCharityId { get; set; }
        public IEnumerable<SelectListItem> Charities { get; set; }
    }
}