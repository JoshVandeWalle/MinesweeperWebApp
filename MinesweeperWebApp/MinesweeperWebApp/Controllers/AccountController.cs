using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinesweeperWebApp.Models;
using MinesweeperWebApp.Services.Business;

namespace MinesweeperWebApp.Controllers
{
    public class AccountController : Controller
    {
        // this code is automatically generated
        public ActionResult Index()
        {
            return View();
        }

        // return the login form
        public ActionResult ShowLoginForm()
        {
            return View("ShowLoginForm");
        }

        // authenticate the user
        public ActionResult Authenticate(CredentialModel credentialSet)
        {
            // instantiate the business service
            UserBusinessService BusinessService = new UserBusinessService();
            // use business service to authenticate user
            bool loginSuccess = BusinessService.Authenticate(credentialSet);

            // if login was successful return the success view
            if (loginSuccess)
            {
                return View("loginSuccess");
            }

            // otherwise return the login fail view
            else
            {
                return View("loginFail");
            }
        }

        // return registration form
        public ActionResult ShowRegistrationForm()
        {
            return View("ShowRegistrationForm");
        }

        // attempt to make a new account
        public ActionResult MakeAccount(UserModel user)
        {
            // instantiate business service 
            UserBusinessService BusinessService = new UserBusinessService();
            // use business service to register user
            bool registerSuccess = BusinessService.MakeAccount(user);

            // if registration succeeds return the success view
            if (registerSuccess)
            {
                return View("registerSuccess");
            }

            // otherwise return the registration failure view
            else
            {
                return View("registerFail");
            }
        }
    }
}