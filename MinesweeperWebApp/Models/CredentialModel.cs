using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinesweeperWebApp.Models
{
    /*
     * CredentialModel is used for sign-in
     */
    public class CredentialModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}