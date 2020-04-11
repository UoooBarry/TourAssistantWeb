using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourWebApp.Models
{
    public class LocationSet
    {
        public int LocationSetID { get; set; }

        [Required]
        public string LocationSetName{get;set;}

        public virtual List<Location> Locations { get; set; }

        public virtual List<Tour> Tour { get; set; }
    }
}
