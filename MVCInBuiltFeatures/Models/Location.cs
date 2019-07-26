using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MVCInBuiltFeatures.Models
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

        [Display(Name = "Client")]
        public int ClientId { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

        [ForeignKey("CharityId")]        
        public virtual Charity Charity { get; set; }
        [Display(Name = "Charity")]
        public int CharityId { get; set; }
        public IEnumerable<SelectListItem> Charities { get; set; }

        [ForeignKey("FranchiseId")]
        public virtual Franchise Franchise { get; set; }
        public int FranchiseId { get; set; }

        [NotMapped]
        public string Distance { get; set; }
    }
}