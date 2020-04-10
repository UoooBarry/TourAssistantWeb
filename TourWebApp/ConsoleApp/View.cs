using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleHashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourWebApp.Data;

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
5. Log out
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
                         
                            break;
                        case 2:
                       
                            break;
                        case 3:
                            AddLocations();
                            break;
                        case 4:
                        
                            break;
                        case 5:
                            
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
                command.CommandText = "insert into dbo.Locations(TourID, Name, X, Y, Description, MinTime) values(111, @Name, @X, @Y, @Description, @MinTime)";
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
            return @"Server=s3673712.database.windows.net;Database=SEPM;uid=s3673712;pwd=Bach12345;MultipleActiveResultSets=true";
        }

    }

}
