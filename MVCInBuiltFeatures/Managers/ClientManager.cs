using MVCInBuiltFeatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCInBuiltFeatures.Managers
{
    public class ClientManager
    {
        public Client GetClient(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                Client client = context.Clients.FirstOrDefault(x => x.Id == id);
                return client;
            }
        }

        public void AddNewClient(AddClientViewModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                var client = new Client
                {
                    Name = model.Name,
                    Address = model.Address,
                    Contact = model.Contact,
                    Email = model.Email,
                    Notes = model.Notes
                };

                context.Clients.Add(client);
                context.SaveChanges();                
            }
        }
        
    }
}