using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCInBuiltFeatures.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCInBuiltFeatures.Managers
{
    public class LocationManager
    {
        public List<Location> GetAllFranchiseLocations(int franchiseId)
        {
            using (var context = new ApplicationDbContext())
            {
                var locations = context.Locations
                                       .Where(x => x.FranchiseId == franchiseId)
                                       .ToList();

                foreach (Location location in locations)
                {
                    location.Distance = "69Km";
                }

                return locations;
            }
        }

        public void AddNewFranchiseLocation(int franchiseId, AddLocationViewModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                var location = new Location
                {
                    Name = model.Name,
                    Address = model.Address,
                    ClientId = model.SelectedClientId,
                    FranchiseId = franchiseId,
                    CharityId = model.SelectedCharityId
                };

                context.Locations.Add(location);
                context.SaveChanges();
            }
        }

        public List<SelectListItem> GetClientList()
        {
            using (var context = new ApplicationDbContext())
            {
                var clientList = context.Clients.ToList().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name

                }).ToList();

                var clientTip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Client ---"
                };

                clientList.Insert(0, clientTip);
                return clientList;
            }     
        }

        public List<SelectListItem> GetCharityList()
        {
            using (var context = new ApplicationDbContext())
            {
                var charityList = context.Charity.ToList().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name

                }).ToList();

                var charityTip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Charity ---"
                };

                charityList.Insert(0, charityTip);
                return charityList;
            }
        }

        public void SaveLocation(Location location)
        {
            using (var context = new ApplicationDbContext())
            {
                Location editableLocation = context.Locations.FirstOrDefault(x => x.Id == location.Id);
                editableLocation.Name = location.Name;
                editableLocation.Address = location.Address;
                editableLocation.Client = location.Client;
                editableLocation.Charity = location.Charity;

                context.SaveChanges();                
            }
        }

        public Location GetLocation(int id, ApplicationUser currentUser)
        {
            using (var context = new ApplicationDbContext())
            {
                Location location = context.Locations.FirstOrDefault(x => x.FranchiseId == currentUser.FranchiseId && x.Id == id);
                return location;
            }
        }

        public Location EditLocation(int id, ApplicationUser currentUser)
        {
            using (var context = new ApplicationDbContext())
            {
            
                Location location = GetLocation(id, currentUser);

                var clientList = GetClientList();
                var charityList = GetCharityList();

                location.Clients = clientList;
                location.Charities = charityList;

                return location;
            }
        }

        public AddLocationViewModel AddLocation()
        {
            using (var context = new ApplicationDbContext())
            {
                AddLocationViewModel location = new AddLocationViewModel();
                
                var clientList = GetClientList();
                location.Clients = clientList;

                var charitiesList = GetCharityList();
                location.Charities = charitiesList;

                return location;
            }
        }

    }
}