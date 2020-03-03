using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourWebApp.Models
{
    public class Tour
    {
        public int TourID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required]
        public virtual TourType Type { get; set; }

        [Required]
        public TimeSpan MinDuration { get; set; }

        public virtual List<Location> Location { get; set; }
    }
}
