using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;

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
        private readonly IHostingEnvironment hostingEnvironment;

        public AccountController(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManger, IUserRepository userRepository ,IHostingEnvironment hostingEnvironment) //using the constructor to add User manager ( for  identity user) and sign in manager services
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



        private string Processuploadfile(EditUserViewModel model)
        {
            // If the Photo property on the incoming model object is not null, then the user
            // has selected an image to upload.
            string UniqueFile = null;
            if (model.photo != null)
            {

                // The image must be uploaded to the images folder in wwwroot
                // To get the path of the wwwroot folder we are using the inject
                // HostingEnvironment service provided by ASP.NET Core
                // string UploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                string UploadsFolder = "C:\\Users\\Adham Moamer\\source\\repos\\Ecommerce2020_base\\wwwroot\\Images";

                // To make sure the file name is unique we are appending a new
                // GUID value and and an underscore to the file name
                UniqueFile = Guid.NewGuid().ToString() + "_" + model.photo.FileName;
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                string Filepath = Path.Combine(UploadsFolder, UniqueFile);
                model.photo.CopyTo(new FileStream(Filepath, FileMode.Create));
            }

            return UniqueFile;
        }

        [HttpGet]
        public async Task<IActionResult> Edit_profile()
        {

            ApplicationUser user1 = await userManger.FindByIdAsync(User.Identity.GetUserId());
            EditUserViewModel model = new EditUserViewModel()
            {
              id = user1.Id,
              FirstName = user1.FirstName,
              LastName= user1.LastName,
              City=user1.City,
              Address=user1.Address,
              ZipCode= user1.ZipCode,
              excistingphotopath = user1.UserPhoto
              

            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit_profile(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user1 = await userManger.FindByIdAsync(User.Identity.GetUserId());

                user1.FirstName = model.FirstName;
                user1.LastName = model.LastName; 
                user1.City = model.City;
                user1.Address = model.Address;
                user1.ZipCode = model.ZipCode;
               
                if (model.photo != null)
                {
                    user1.UserPhoto = Processuploadfile(model);
                }
                var result = await userManger.UpdateAsync(user1);

                // If user is successfully created, sign-in the user using
                // SignInManager and redirect to index action of HomeController
                if (result.Succeeded)
                {

                    return View("Account","profile");

                }
                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


            }
            return View();
        }

    }
}
