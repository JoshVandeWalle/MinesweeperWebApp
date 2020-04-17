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
        // the logger. This dependency will be injected
        Services.Utility.ILogger Logger;

        /*
         * non-default constructor used dependency injection 
         */
        public AccountController(Services.Utility.ILogger logger)
        {
            Logger = logger;
        }

        // return the login form
        public ActionResult ShowLoginForm()
        {
            try
            {
                return View("ShowLoginForm");
            }

            catch(Exception e)
            {
                Logger.Error("Failed loading login form view: " + e.Message);
                // TODO make common error page
                return View();
            }
        }

        // authenticate the user
        public ActionResult Authenticate(CredentialModel credentialSet)
        {
            try
            {
                //log entry into method
                Logger.Info("Entering AccountController.Authenticate()");

                // instantiate the business service
                UserBusinessService BusinessService = new UserBusinessService();

                // use business service to authenticate user
                bool loginSuccess = BusinessService.Authenticate(credentialSet);

                // if login was successful return the success view
                if (loginSuccess)
                {
                    Cache.AccessCache().Put("activeAccount", credentialSet.Email);

                    //log success
                    Logger.Info("Exiting AccountController.Authenticate() with login success");

                    return View("loginSuccess");
                }

                // otherwise return the login fail view
                else
                {
                    //log failure
                    Logger.Info("Exiting AccountController.Authenticate() with login failure");

                    return View("loginFail");
                }
            }

            catch (Exception e)
            {
                Logger.Error("Exiting AccountController.Authenticate() with an exception: " + e.Message);
                return Content("Exception in login: " + e.Message);
            }
        }

        // return registration form
        public ActionResult ShowRegistrationForm()
        {
            try
            {
                return View("ShowRegistrationForm");
            }

            catch (Exception e)
            {
                Logger.Error("Failed loading registration form view: " + e.Message);
                // TODO make common error page
                return View();
            }
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
                Logger.Info("Registration successful");
                return View("registerSuccess");
            }

            // otherwise return the registration failure view
            else
            {
                Logger.Error("Registration unsuccessful");
                return View("registerFail");
            }
        }
    }
}