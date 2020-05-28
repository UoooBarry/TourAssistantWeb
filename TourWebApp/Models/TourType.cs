using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourWebApp.Models
{
    public class TourType
    {
        public int TourTypeID { get; set; }

        [Required, StringLength(50)]
        public string Label { get; set; }

        public virtual List<Tour> Tour { get; set; }
    }
}
