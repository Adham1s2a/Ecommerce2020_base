using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AppDbContext context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManger;
        private readonly SignInManager<ApplicationUser> signInManger;
        private readonly IUserRepository UserRepository;
        
        public AccountController(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManger,IUserRepository userRepository ) //using the constructor to add User manager ( for  identity user) and sign in manager services
        {
           
            this.userManger = userManger;
            this.signInManger = signInManger;
            this.UserRepository = userRepository;
        }


        // GET: /<controller>/
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                // Copy data from RegisterViewModel to ApplicationUser
                ApplicationUser user = new ApplicationUser()
                {

                    UserName = model.UserName,
                    Email = model.Email,
                    City = model.City,
                    PhoneNumber = model.phoneNumber,
                    ZipCode = model.ZipCode,
                    FirstName=model.FirstName,
                    LastName=model.LastName,
                    Address=model.Address



                };
                // Store user data in AspNetUsers database table
                var result = await userManger.CreateAsync(user, model.Password);

                // If user is successfully created, sign-in the user using
                // SignInManager and redirect to index action of HomeController
                if (result.Succeeded)
                {
                    await signInManger.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


            }
            return View(model);
        }

        //check if email is in use 
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManger.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use.");
            }

        }


        ////////////////////Logout action
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManger.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        ///////////////////Login action
        ///
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult test()
        {
            return View();
        }
        [HttpPost]

        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                if (model.Email.IndexOf('@') > -1)
                {
                    //Validate email format
                    string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                    Regex re = new Regex(emailRegex);
                    if (!re.IsMatch(model.Email))
                    {
                        ModelState.AddModelError("Email", "Email is not valid");
                    }
                }
                else
                {
                    //validate Username format
                    string emailRegex = @"^[a-zA-Z0-9]*$";
                    Regex re = new Regex(emailRegex);
                    if (!re.IsMatch(model.Email))
                    {
                        ModelState.AddModelError("Email", "Username is not valid");
                    }
                }

                if (ModelState.IsValid)
                {
                    var userName = model.Email;
                    if (userName.IndexOf('@') > -1)
                    {
                        var user = await userManger.FindByEmailAsync(model.Email);
                        if (user == null)
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return View(model);
                        }
                        else
                        {
                            userName = user.UserName;
                        }
                    }
                    var result = await signInManger.PasswordSignInAsync(userName, model.Password, model.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("index", "home");
                        }
                    }
                }
                 

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }

            return View(model);
        }
        //profile
        [HttpPost][HttpGet]
        public async Task<IActionResult> profile()
        {
           ApplicationUser user1 = await userManger.FindByIdAsync(User.Identity.GetUserId());


            return View(user1);
        }

        public  IActionResult test2()
        {
            return View();

        }
       

    }
}
