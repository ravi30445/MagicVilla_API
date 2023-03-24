using MagicVilla_webapi.Models;
using Microsoft.EntityFrameworkCore;
using MagicVilla_VillaAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MagicVilla_webapi.data{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){
            
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<LocalUser>LocalUsers{get;set;}
        public DbSet<Villa>Villa{get;set;}
        public DbSet<VillaNumber> VillaNumbers{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    id = 1,
                    name = "Royal Villa",
                    details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  
                    occupancy = 4,
                    rate = 200,
                    sqft = 550
                },
              new Villa
              {
                  id = 2,
                  name = "Premium Pool Villa",
                  details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
           
                  rate = 300,
                  sqft = 550
              },
              new Villa
              {
                  id = 3,
                  name = "Luxury Pool Villa",
                  details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                 
                  occupancy = 4,
                  rate = 400,
                  sqft = 750
              },
              new Villa
              {
                  id = 4,
                  name = "Diamond Villa",
                  details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
               
                  occupancy = 4,
                  rate = 550,
                  sqft = 900
              },
              new Villa
              {
                  id = 5,
                  name = "Diamond Pool Villa",
                  details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                 
                  occupancy = 4,
                  rate = 600,
                  sqft = 1100
              });
    }
}
}
