using Freelancer_Exam.Entities;
using Freelancer_Exam.Services.Abstract;
using Freelancer_Exam.ViewModels;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> userManager;

        public HomeController(IHomeService homeService, UserManager<User> userManager)
        {
            this.homeService = homeService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(string search)
        {
            List<Project> projects = null;
            if (search == null) projects = homeService.GetAllProjects();
            else projects = homeService.GetProjectsBySearch(search);
            return View(projects);
        }

        [HttpGet]
        public async Task<IActionResult> ProjectDetails([FromQuery]string projectId)
        {
            var projectDetails = homeService.GetProjectById(projectId);
            if (projectDetails == null) return Content("<h1>Invalid Request</h1>", "text/html");

            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                var userType = homeService.GetUserType(user.Id);
                projectDetails.UserType = userType;
                projectDetails.UserId = user.Id;
            } 
            else
            {
                projectDetails.UserType = Models.UserType.NotAuthorized;                
            }
            return View(projectDetails);
        }        
    }
}
