using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinesweeperWebApp.Models;
using MinesweeperWebApp.Services.Business;
using MinesweeperWebApp.Services.Utility;
using NLog;

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
            try
            {
                

                //log entry into method
                MyLogger.GetInstance().Info("Entering AccountController.Authenticate()");

                // instantiate the business service
                UserBusinessService BusinessService = new UserBusinessService();

                // use business service to authenticate user
                bool loginSuccess = BusinessService.Authenticate(credentialSet);

                // if login was successful return the success view
                if (loginSuccess)
                {
                    Cache.AccessCache().Put("activeAccount", credentialSet.Email);

                    //log success
                    MyLogger.GetInstance().Info("Exiting AccountController.Authenticate() with login success");

                    return View("loginSuccess");
                }

                // otherwise return the login fail view
                else
                {
                    //log failure
                    MyLogger.GetInstance().Info("Exiting AccountController.Authenticate() with login failure");

                    return View("loginFail");
                }
            }

            catch (Exception e)
            {
                MyLogger.GetInstance().Error("Exiting AccountController.Authenticate() with an exception: " + e.Message);
                return Content("Exception in login" + e.Message);
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