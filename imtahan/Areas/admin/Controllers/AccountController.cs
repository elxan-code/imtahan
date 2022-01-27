using imtahan.Models;
using imtahan.VwModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imtahan.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser>userManager,SignInManager<IdentityUser>signInManager)
        {
            _userManager = userManager;
           _signInManager = signInManager;
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(VmRegister model)
        {
            if (ModelState.IsValid)
            {
                CustomUser user = new CustomUser()
                {
                    Fullname = model.FullName,
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("index", "home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                      
                    }
                }
                return View(model);
            }
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(VmLogin model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ModelState.AddModelError("", "Email Is not Valid");
                    return View(model);
                }
            }
            return View(model);

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }

    }
}
