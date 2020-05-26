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
        public int TourTypeID { get; set; }
        public virtual TourType Type { get; set; }

        [Required]
  //      public TimeSpan MinDuration { get; set; }
        public virtual IList<Location_Tour> Location_Tour { get; set; }
    }
}
