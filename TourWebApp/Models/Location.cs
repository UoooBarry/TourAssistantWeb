using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourWebApp.Models
{
    public class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LocationID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public float X { get; set; }
        [Required]
        public float Y { get; set; }

        [Required, StringLength(300)]
        public string Description { get; set; }
        [Required]
        public TimeSpan MinTime { get; set; }

        public virtual IList<Location_Tour> Location_Tour { get; set; }

    }
}
