using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourWebApp.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID {get;set;}

        [Required, StringLength(50)]
        public string Name { get; set; }

        public virtual Login Login { get; set; }

        [Required]
        public string Role {get;set;}
    }
}
