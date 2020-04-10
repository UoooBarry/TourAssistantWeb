using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourWebApp.ConsoleApp
{
    public class View
    {
        public void Display()
        {
            String output = "";
            string connetionString = @"Server=s3673712.database.windows.net;Database=SEPM;uid=s3673712;pwd=Bach12345;MultipleActiveResultSets=true";
            SqlConnection connect = new SqlConnection(connetionString);
            connect.Open();
            SqlCommand command = new SqlCommand("Select LoginID,UserID from dbo.Logins", connect);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read()) {
                
                output = output + "LoginID: " + dr.GetValue(0) + " - UserID: " + dr.GetValue(1);
            }
            while (true)
            {
                Console.WriteLine(output);
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
