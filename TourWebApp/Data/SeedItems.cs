﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourWebApp.Models;

namespace TourWebApp.Data
{
    public class SeedItems
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new TourContext(serviceProvider.GetRequiredService<DbContextOptions<TourContext>>());
            // Look for users.
            if (context.Users.Any())
                return; // DB has already been seeded.
            context.Users.AddRange(
            new User
            {
                Name = "Test user",
                Role = "Admin"
            },
            new User
            {
                Name = "Barry Huang",
                Role = "Admin"
            }
            );

          /*  context.Logins.AddRange
            (
                new Login
                {
                    LoginID = 12345678,
                    PasswordHash = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2",
                    ActivationStatus = true,
                    UserID = context.Users.FirstOrDefault().UserID
                }

            );*/
            context.TourTypes.Add
             (
                new TourType
                {
                    Label = "Default"
                }
             );

            context.Locations.AddRange
            (
                new Location
                {
                    Name = "Music ground",
                    X = 3,
                    Y = 4,
                    Description = "A place that you can play music",
                    MinTime = new TimeSpan(0,10,0), //10 mins

                },
                new Location
                {
                    Name = "Photo area",
                    X = 3,
                    Y = 4,
                    Description = "A place that you can take photo",
                    MinTime = new TimeSpan(0, 10, 0), //10 mins
                }
            );
            context.SaveChanges();
        }

    }
}
