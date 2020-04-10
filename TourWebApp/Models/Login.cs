using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourWebApp.Models
{
    public class Login
    {
        [Required, StringLength(8)]
        [Display(Name = "Login ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LoginID { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        public bool ActivationStatus { get; set; }

        [Required]
        public int UserID { get; set; }
        public virtual User user { get; set; }
    }
}
