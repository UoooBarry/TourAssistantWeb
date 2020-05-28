using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourWebApp.Models
{
    public class Location_Tour
    {
        [Key]
        [Required]
        public int Location_TourID { get; set; }

        [ForeignKey("Tour")]
        public int TourID { get; set; }
        public virtual Tour Tour { get; set; }

        [ForeignKey("Location")]
        public int LocationID { get; set; }
        public virtual Location Location { get; set; }
    }
}
