using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MinesweeperWebApp.Models;
using MinesweeperWebApp.Services.Data;

namespace MinesweeperWebApp.Services.Business
{
    /* 
     * This is a business service that handles business rules and logic
     */
    public class UserBusinessService
    {
        // Authenticate method is used for login
        public bool Authenticate(CredentialModel CredentialSet)
        {
            UserDataService DataService = new UserDataService();
            return DataService.ReadAccount(CredentialSet);
        }

        // MakeAccount method is used for registration
        public bool MakeAccount(UserModel User)
        {
            UserDataService DataService = new UserDataService();
            return DataService.CreateAccount(User);
        }
    }
}