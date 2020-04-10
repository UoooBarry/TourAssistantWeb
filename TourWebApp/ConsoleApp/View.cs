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
        

    }

}
