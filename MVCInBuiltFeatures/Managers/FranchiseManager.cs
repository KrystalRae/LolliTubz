using MVCInBuiltFeatures.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCInBuiltFeatures.Managers
{
    public class FranchiseManager
    {      
        public List<SelectListItem> GetFranchiseList()
        {
            using (var context = new ApplicationDbContext())
            {
                var franchiseList = context.Franchise.ToList().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name

                }).ToList();
                return franchiseList;
            }
        }

    }
}