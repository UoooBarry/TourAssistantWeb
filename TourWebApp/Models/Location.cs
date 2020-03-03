using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourWebApp.Models
{
    public enum Coordinate 
    {
        X,
        Y
    }

    public class Location
    {
        public int LocationID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public Dictionary<Coordinate,float> Coordinate { get; set; }

        [Required, StringLength(300)]
        public string Description { get; set; }
        [Required]
        public TimeSpan MinTime { get; set; }

        [Required]
        public int TourID { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
