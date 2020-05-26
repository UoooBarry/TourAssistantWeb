using Microsoft.AspNetCore.Razor.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using TourWebApp.Models;
using Xunit;

namespace ConsoleAppTests
{
    public class Sprint1UnitTests
    {
        public Location defaultLocation;
        public TourType defaultTourType;
        public Sprint1UnitTests()
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

    }
}
