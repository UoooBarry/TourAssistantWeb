using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourWebApp.Views
{
    public class Menu
    {
        private int? LoginCustomerID { get; set; }

        public void displayMenu() 
        {
            while (true)
            {
                Console.Write(
    @"==============================================
WELCOME TO National Wealth Bank of Australasia
Select the application that want to run
==============================================
1. Login
2. Quit
");
                var input = Console.ReadLine();
                int number;
                if (!Int32.TryParse(input, out number))
                {
                    Console.WriteLine("Invalid intput");
                    continue;
                }

                switch (number)
                {
                    case 1:
                        //login;
                        
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
    }
}
