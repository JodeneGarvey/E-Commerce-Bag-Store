using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC2_1601226.Models;
using EC2_1601226.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EC2_1601226.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        //[Route("signup")]
        [AllowAnonymous]
        public IActionResult Signup()
        {
            return View();
        }

        

        //[Route("signup")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(SignUpUserModel userModel)
        {
            if(ModelState.IsValid)
            {
                //code
                var result = await _accountRepository.CreateUserAsync(userModel);
                if(!result.Succeeded)
                {
                    if(_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }
                    foreach(var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }

                    return View();
                }
                
                ModelState.Clear();
            }
            return View();
        }

        //[Route("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        //[Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(SignInModel signInModel, string returnUrl)
        {
            if(ModelState.IsValid)
            {

                var result = await _accountRepository.PasswordSignInAsync(signInModel);

                if(result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                }

                ModelState.AddModelError("", "Username or Password Incorrect");
            }
            return View(signInModel);
        }

        //[Route("logout")]

        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
    
}