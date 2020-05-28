using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleHashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourWebApp.Data;
using TourWebApp.Models;

namespace TourWebApp.ConsoleApp
{

    public class View
    {
        IServiceProvider serviceProvider;

        public View(IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider;
        }
        public void Display()
        {
            while (true)
            {
                Console.Write(
 @"==============================================
WELCOME TO Tour Management System
Select the application that want to run
==============================================
1. Login
2. Quit
");
                var selection = Console.ReadLine();
                int number;
                if (!Int32.TryParse(selection, out number))
                {
                    Console.WriteLine("Invalid intput");
                    continue;
                }
                switch (number)
                {
                    case 1:
                        displayMenu();
                        break;
                    case 2:
                        return;
                    default:
                        Console.WriteLine("Invalid Input");
                        Console.WriteLine("Press any key to go back . . .");
                        Console.ReadKey();
                        Console.WriteLine("");
                        break;
                }


            }
        }

        public async Task<bool> Login(int ID, string password)
        {
            using var context = new TourContext(serviceProvider.GetRequiredService<DbContextOptions<TourContext>>());
            var login = await context.Logins.FindAsync(ID);
            if (login == null || !PBKDF2.Verify(login.PasswordHash, password))
            {
                return false;
            }
            else {
                return true;
            }
        }
        public void displayMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("Input an ID");
            int ID = int.Parse(Console.ReadLine());
            Console.WriteLine("Input an password");
            string password = Console.ReadLine();
            if (Login(ID, password).Result)
            {
                Console.WriteLine("You are now login");
                while (true)
                {
                    Console.WriteLine(
        @"Select the application that want to run
=========================================
1. View Locations
2. View Tours
3. Add Locations
4. Add Tours
5. Remove Tour
6. Remove Location
7. Log out
");
                    var selection = Console.ReadLine();
                    Console.WriteLine("");

                    int number;
                    if (!Int32.TryParse(selection, out number))
                    {
                        Console.Write("Invalid intput");
                        continue;
                    }
                    switch (number)
                    {
                        case 1:
                            Console.WriteLine(GetLocations());
                            break;
                        case 2:
                            Console.WriteLine(GetTours());
                            break;
                        case 3:
                            AddLocations();
                            break;
                        case 4:
                            AddTours();
                            break;
                        case 5:
                            RemoveTour();
                            break;
                        case 6:
                            RemoveLocatonAsync();
                            break;
                        case 7:
                            return;
                        default:
                            Console.WriteLine("Invalid Input");
                            Console.WriteLine("Press any key to go back . . .");
                            Console.ReadKey();
                            Console.WriteLine("");
                            break;
                    }
                }
            }
            else
                Console.WriteLine("Login Fail");
        }


        public string GetTours()
        {
            using var context = new TourContext(serviceProvider.GetRequiredService<DbContextOptions<TourContext>>());
            List<Tour> tours = context.Tours.ToList();
             string result = "";
            foreach (Tour tour in tours) 
            {
                result += "Name: " + tour.Name + ", Type: " + tour.Type.Label + " ,MinDuration: " + tour.MinDuration + "\n";
            }
            return result;
        }

        public void AddTours()
        {
            using var context = new TourContext(serviceProvider.GetRequiredService<DbContextOptions<TourContext>>());

            Console.WriteLine("Enter tour name: ");
            string name = Console.ReadLine();

            Console.WriteLine("How many locations you want to add into the tour:");
            int count = 0;
            try
            {
                count = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                count = Convert.ToInt32(Console.ReadLine());
            }

            List<Location> locations = new List<Location>();
            for (int i = 0; i < count; i++) 
            {
                Console.WriteLine($"Add the {i + 1} location's Id");
                int id = Convert.ToInt32(Console.ReadLine());
                locations.Add(context.Locations.Find(id));
            }

            Tour tour = new Tour
            {
                Name = name,
                TourTypeID = 1//seed item for console app
            };

            List<Location_Tour> lcs = new List<Location_Tour>();
            foreach (Location location in locations) 
            {
                var Location_Tour = new Location_Tour
                {
                    TourID = tour.TourID,
                    LocationID = location.LocationID
                };
                context.LocationSets.Add(Location_Tour);
                lcs.Add(Location_Tour);
            }
            tour.Location_Tour = lcs;
            tour.MinDuration = tour.caculateMinDuration();

            context.Tours.Add(tour);
            context.SaveChanges();
        }

        public void RemoveTour() 
        {
            using var context = new TourContext(serviceProvider.GetRequiredService<DbContextOptions<TourContext>>());
            Console.WriteLine("Enter the tour ID you want to remove:");
            int Id = 0;
            try
            {
                Id = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Id = Convert.ToInt32(Console.ReadLine());
            }

            context.Tours.Remove(context.Tours.Find(Id));
            context.SaveChanges();
        }

        public void RemoveLocatonAsync()
        {
            using var context = new TourContext(serviceProvider.GetRequiredService<DbContextOptions<TourContext>>());
            Console.WriteLine("Enter the tour Location you want to remove:");
            int Id = 0;
            try
            {
                Id = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Id = Convert.ToInt32(Console.ReadLine());
            }

            context.LocationSets.RemoveRange(context.LocationSets.Where(e => e.Location.LocationID == Id));
            context.Locations.Remove(context.Locations.Find(Id));
            context.SaveChanges();
        }

        private string GetLocations() 
        {
            using var context = new TourContext(serviceProvider.GetRequiredService<DbContextOptions<TourContext>>());
            List<Location> locations = context.Locations.ToList();
            string result = "";
            foreach (Location location in locations) 
            {
                result += "Id: "+ location.LocationID  + " ,Name: " + location.Name + " ,Descriptions: " + location.Description + "\n";
            }
            return result;
        }
        private void AddLocations() {
            while (true)
            {
                Console.WriteLine("Enter name:");
                string tourName = Console.ReadLine();
                Console.WriteLine("Enter coordination X:");
                string x = Console.ReadLine();
                Console.WriteLine("Enter coordination Y:");
                string y = Console.ReadLine();
                Console.WriteLine("Enter Description:");
                string d = Console.ReadLine();
                Console.WriteLine("Enter duration:");
                string t = Console.ReadLine();
                SqlCommand command = getSQLCommand();
                command.CommandText = "insert into dbo.Locations(LocationID, Name, X, Y, Description, MinTime) values(111, @Name, @X, @Y, @Description, @MinTime)";
                command.Parameters.AddWithValue("Name", tourName);
                command.Parameters.AddWithValue("X", x);
                command.Parameters.AddWithValue("Y", y);
                command.Parameters.AddWithValue("Description", d);
                command.Parameters.AddWithValue("MinTime", TimeSpan.Parse(t));
                command.ExecuteNonQuery();
                break;
            }

        }
        protected SqlCommand getSQLCommand()
        {

            var command = GetConnection().CreateCommand();
            return command;
        }
        protected SqlConnection GetConnection()
        {
            var db = new SqlConnection(getConnectionString());
            db.Open();
            return db;
        }
        protected string getConnectionString()
        {
            return @"Server=tourdb.cosjhthqukoc.us-east-1.rds.amazonaws.com, 1433;Database=Tour;uid=admin;pwd=tingting0417;MultipleActiveResultSets=true";
        }

    }

}
