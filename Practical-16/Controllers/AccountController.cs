using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Practical_16.Models;

namespace Practical_16.Controllers
{
    public class AccountController : Controller
    {
        UserDataAccessLayer objUser = new UserDataAccessLayer();

        [HttpGet]
        public IActionResult RegisterUser()
        {

            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser([Bind] User user)
        {
            if (ModelState.IsValid)
            {
                string RegistrationStatus = objUser.RegisterUser(user);
                if (RegistrationStatus == "Success")
                {
                    ModelState.Clear();
                    TempData["Success"] = "Registration Successful!";
                    return View();
                }
                else
                {
                    TempData["Fail"] = "This User ID already exists. Registration Failed.";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult UserLogin()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin([Bind] User user)
        {
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");

            if (ModelState.IsValid)
            {
                string LoginStatus = objUser.ValidateLogin(user);

                if (LoginStatus == "Success")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserID),
                        new Claim(ClaimTypes.Role,"User")
                    };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Students");
                }

                else 
                {
                    TempData["UserLoginFailed"] = "Login Failed.Please enter correct credentials";
                    return View();
                }
            }
            else
                return View();

        }
        public IActionResult RoleBaseLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            //Check the user name and password  
            //Here can be implemented checking logic from the database  
            ClaimsIdentity identity = null;
            bool isAuthenticated = false;
            if (userName == "Admin" && password == "password")
            {

                //Create the identity for the user  
                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticated = true;
            }


            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Students");
            }

            return View();
        }


        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}