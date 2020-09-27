using Freelancer_Exam.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var projects = homeService.GetAllProjects();
            return View(projects);
        }
        [HttpGet]
        public IActionResult ProjectDetails([FromQuery]string projectId)
        {
            var projectDetails = homeService.GetProjectById(projectId);
            if (projectDetails == null) return Content("<h1>Invalid Request</h1>", "text/html");
            return View(projectDetails);
        }
    }
}
