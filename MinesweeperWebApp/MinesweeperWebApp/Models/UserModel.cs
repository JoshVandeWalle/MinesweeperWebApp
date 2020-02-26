using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MinesweeperWebApp.Models
{
    /*
     * UserModel is used for registration and all User CRUD functionality
     */
    public class UserModel
    {
        // User info

        [Display(Name = "First Name")]
        [StringLength(40, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(40, MinimumLength = 3)]
        public string LastName { get; set; }

        [StringLength(40, MinimumLength = 1)]
        public string Sex { get; set; }

        
        public int Age { get; set; }

        [StringLength(40, MinimumLength = 1)]
        public string State { get; set; }

        [StringLength(40, MinimumLength = 3)]
        public string Username { get; set; }

        [StringLength(40, MinimumLength = 3)]
        public string  Email { get; set; }

        [StringLength(40, MinimumLength = 3)]
        public string Password { get; set; }
    }
}