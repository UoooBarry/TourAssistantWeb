using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourWebApp.Models
{
    public class User
    {
        public int UserID {get;set;}

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required]
        public virtual Login Login { get; set; }
    }
}
