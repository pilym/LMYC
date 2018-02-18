namespace LmycWebSite.Migrations.IdentityMigrations
{
    using LmycDataLib.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LmycDataLib.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\IdentityMigrations";
        }

        protected override void Seed(LmycDataLib.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // create roles
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            if (!roleManager.RoleExists("Member"))
            {
                roleManager.Create(new IdentityRole("Member"));
            }

            // create users
            // dummy data
            string[] usernames = { "a", "m", };
            string[] emails = { "a@a.a", "m@m.m" };
            string password = "P@$$w0rd";
            string[] names = { "Admin", "Admin", "Test", "User" };
            string[] streets = { "Someplace", "Oak Road" };
            string[] cities = { "Somewhere", "Port City" };
            string province = "Canadian North Pole Territory";
            string postalcode = "H0H 0H0";
            string country = "Canada";
            string mobileNo = "5551234567";
            string[] sailingExp = { "none", "2 years" };

            // add admin
            if (userManager.FindByEmail(emails[0]) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = usernames[0],
                    Email = emails[0],
                    FirstName = names[0],
                    LastName = names[1],
                    AddressStreet = streets[0],
                    AddressCity = cities[0],
                    AddressProvince = province,
                    AddressPostalCode = postalcode,
                    AddressCountry = country,
                    MobileNumber = mobileNo,
                    SailingExperience = sailingExp[0]
                };

                var result = userManager.Create(user, password);

                if (result.Succeeded)
                {
                    userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Admin");
                }
            }

            // add member
            if (userManager.FindByEmail(emails[1]) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = usernames[1],
                    Email = emails[1],
                    FirstName = names[2],
                    LastName = names[3],
                    AddressStreet = streets[1],
                    AddressCity = cities[1],
                    AddressProvince = province,
                    AddressPostalCode = postalcode,
                    AddressCountry = country,
                    MobileNumber = mobileNo,
                    SailingExperience = sailingExp[1]
                };

                var result = userManager.Create(user, password);

                if (result.Succeeded)
                {
                    userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Member");
                }
            }
        }
    }
}
