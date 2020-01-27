using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinesweeperWebApp.Models
{
    /*
     * UserModel is used for registration and all User CRUD functionality
     */
    public class UserModel
    {
        // User info
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string State { get; set; }
        public string Username { get; set; }
        public string  Email { get; set; }
        public string Password { get; set; }
    }
}