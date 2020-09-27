using System;
using System.Threading.Tasks;
using Freelancer_Exam.Entities;
using Freelancer_Exam.Services.Abstract;
using Freelancer_Exam.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Freelancer_Exam.Controllers {
    public class AccountController : Controller {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService, UserManager<User> userManager) {
            _accountService = accountService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Login() {
            if (HttpContext.User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Register() {
            if (HttpContext.User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut() {
            await _accountService.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {
            if (ModelState.IsValid) {
                var result = await _accountService.SignIn(model);
                if (result.Success) {
                    return RedirectToAction("Profile","Developer"); // temp
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                var result = await _accountService.SignUp(model);
                if (result.Success) {
                    return RedirectToAction("Login", "Account");
                }
            }

            return View(model);
        }
    }
}