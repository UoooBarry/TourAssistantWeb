using Microsoft.AspNetCore.Razor.Language;
using SimpleHashing;
using System;
using System.Collections.Generic;
using System.Linq;
using TourWebApp.Models;
using Xunit;

namespace ConsoleAppTests
{
    public class UnitTests
    {
        public Location defaultLocation;
        public TourType defaultTourType;
        public User defaultAccount;
        public Login defaultLogin;
        public UnitTests()
        {
            defaultLocation = new Location
            {
                LocationID = 1,
                Name = "Photo area",
                X = 3,
                Y = 4,
                Description = "A place that you can take photo",
                MinTime = new TimeSpan(0, 10, 0), //10 mins
            };

            defaultTourType = new TourType
            {
                Label = "Default"
            };
            defaultAccount = new User
            {
                UserID = 1,
                Name = "test name",
                Role = "admin"

            };
            defaultLogin = new Login
            {
                UserID = 1,
                LoginID = 123456,
                PasswordHash = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2",
                ActivationStatus = true
            };
        }

        [Fact]
        public void AddNewLocation()
        {
            var location = new Location
            {
                LocationID = 9,
                Name = "Test place",
                X = 3,
                Y = 4,
                Description = "Place",
                MinTime = new TimeSpan(0, 10, 0), //10 mins
           };
            Assert.Equal(9, location.LocationID);
            Assert.Equal("Test place", location.Name);
        }
        [Fact]
        public void AddNewUser()
        {
            var user = new User
            {
                UserID = 1,
                Name = "test name",
                Role = "admin"
            };
            Assert.Equal(1, user.UserID);
            Assert.Equal(user.Name, defaultAccount.Name);
        }

        [Fact]
        public void AddNewTourUsingLocation() 
        {
            Tour tour = new Tour
            {
                Name = "New tour",
                TourTypeID = defaultTourType.TourTypeID, //seed item for console app
                Type = defaultTourType
            };

            List<Location_Tour> lcs = new List<Location_Tour>();
            lcs.Add(new Location_Tour
            {
                TourID = tour.TourID,
                LocationID = defaultLocation.LocationID
            });
            tour.Location_Tour = lcs;
            Assert.Equal("Default", tour.Type.Label);
            Assert.Equal(lcs, tour.Location_Tour);
            Assert.True(tour.Location_Tour.Count > 0);
        }

        [Fact]
        public void AddNewUsers() 
        {
            User user = new User
            {
                Name = "Test",
                Role = "Admin"
            };

            Assert.Equal("Test", user.Name);
        }

        [Fact]
        public void LoginPasswordShouldMatchUser()
        {
            User user = new User
            {
                Name = "Test",
                Role = "Admin",
            };

            Login login = new Login
            {
                LoginID = 12345678,
                PasswordHash = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2",
                ActivationStatus = true,
                UserID = user.UserID,
                user = user
            };
           user.Login = login;
           
           Assert.True(PBKDF2.Verify(login.PasswordHash, "abc123"));
           Assert.Equal("Test", login.user.Name);
        }

    }
}
