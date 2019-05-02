﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Linq;

namespace MVCInBuiltFeatures.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new ApplicationInitializer());
        }

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Charity> Charity { get; set; }
        public virtual DbSet<Franchise> Franchise { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class ApplicationInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var administratorRole = new IdentityRole { Name = "Administrator" };
            roleManager.Create(administratorRole);

            var franchiseRole = new IdentityRole { Name = "Franchise" };
            roleManager.Create(franchiseRole);
                       
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var adminUser = new ApplicationUser { UserName = "admin", FranchiseId = -1 };
            userManager.Create(adminUser, "Sh5483GqzTQn22Z");
            userManager.AddToRole(adminUser.Id, "Administrator");          
               
            var craigUser = new ApplicationUser { UserName = "craig", FranchiseId = 1 };
            userManager.Create(craigUser, "Sh5483GqzTQn22Z");
            userManager.AddToRole(craigUser.Id, "Franchise");
           
            context.SaveChanges();

            var franchise1 = new Franchise
            {
                Id = 1,
                Name = "Lollitubz"
            };

            context.Franchise.Add(franchise1);
            context.SaveChanges();

            var client1 = new Client
            {
                Id = 1,
                Name = "Happy Gilmore",
                Address = "1 Happy Client Street",
                Contact = 0490153351,
                Email = "happy.gilmore@gmail.com",
                Notes = "Too happy, this dude is wierd."
            };

            var charity1 = new Charity
            {
                Id = 1,
                Name = "Cancer Foundation"
            };

            context.Clients.Add(client1);
            context.SaveChanges();

            var location1 = new Location
            {
                Id = 1,
                Name = "Fun Company",
                Address = "1 Fun Street",
                Charity = charity1,
                Client = client1,
                Franchise = franchise1
            };

            context.Locations.Add(location1);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}