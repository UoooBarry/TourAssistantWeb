using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourWebApp.Models
{
    public class Location_Tour
    {

        public int TourID { get; set; }
        public virtual Tour Tour { get; set; }

        public int LocationID { get; set; }
        public virtual Location Location { get; set; }
    }
}
