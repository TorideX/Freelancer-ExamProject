using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Freelancer_Exam.Controllers {
    public class AccountController : Controller {
        [HttpGet]
        public async Task<IActionResult> Login() {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Register() {
            return View();
        }
    }
}