using System.Threading.Tasks;
using Freelancer_Exam.Services.Abstract;
using Freelancer_Exam.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Freelancer_Exam.Controllers {
    public class AccountController : Controller {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) {
            _accountService = accountService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Login() {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Register() {
            return View();
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