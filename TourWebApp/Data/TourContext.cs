using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourWebApp.Models;

namespace TourWebApp.Data
{
    public class TourContext : DbContext
    {
        public TourContext(DbContextOptions<TourContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourType> TourTypes { get; set; }
        public DbSet<Location_Tour> LocationSets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) //validation
        {
            base.OnModelCreating(builder);
            builder.Entity<Location_Tour>().HasKey(id => new { id.LocationID , id.TourID });
            builder.Entity<Login>().HasCheckConstraint("CH_Login_LoginID", "len(LoginID) = 8").
                HasCheckConstraint("CH_Login_PasswordHash", "len(PasswordHash) = 64");
            builder.Entity<Tour>().
                HasOne<TourType>(e => e.Type).WithMany(e => e.Tour).HasForeignKey(e=> e.TourTypeID);
            builder.Entity<Login>().
                HasOne<User>(e => e.user).WithOne(e => e.Login).HasForeignKey<Login>(e => e.UserID);

            //Tour_Location
            builder.Entity<Location_Tour>()
                .HasOne<Tour>(e => e.Tour)
                .WithMany(e => e.Location_Tour)
                .HasForeignKey(e => e.TourID);
            builder.Entity<Location_Tour>()
                .HasOne<Location>(e => e.Location)
                .WithMany(e => e.Location_Tour)
                .HasForeignKey(e => e.LocationID);
        }
    }

    
}
