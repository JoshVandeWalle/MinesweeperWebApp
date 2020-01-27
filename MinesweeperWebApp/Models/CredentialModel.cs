using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MinesweeperWebApp.Models
{
    /*
     * CredentialModel is used for sign-in
     */
    public class CredentialModel
    {
        [StringLength(50, MinimumLength = 3)]
        public string Email { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Password { get; set; }
    }
}