using Microsoft.AspNetCore.Mvc;

namespace Freelancer_Exam.Controllers {
    public class OwnerController : Controller {
        // GET
        public IActionResult Index() {
            return View();
        }
    }
}