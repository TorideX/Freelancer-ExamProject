using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Freelancer_Exam.Entities;
using Freelancer_Exam.Entities.Db_Context;
using Freelancer_Exam.Services.Abstract;
using Freelancer_Exam.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Freelancer_Exam.Controllers {
    [Authorize(Roles = "Developer")]
    public class DeveloperController : Controller {
        private readonly IFreelancerService _freelancerService;
        private readonly UserManager<User> _userManager;
        private readonly FreelancerDbContext freelancerDb;

        public DeveloperController(IFreelancerService freelancerService, UserManager<User> userManager, FreelancerDbContext freelancerDb) {
            _freelancerService = freelancerService;
            _userManager = userManager;
            this.freelancerDb = freelancerDb;
        }
        [HttpPost]
        public async Task<IActionResult> UploadProfilePhoto(IFormFile file) {
            var current = Directory.GetCurrentDirectory();
            var imgSrc = Path.Combine(current, "wwwroot", "img", file.FileName);
            await using var stream = new FileStream(imgSrc, FileMode.Create);
            await file.CopyToAsync(stream);
            return Ok(file.FileName);
        }
        
        [HttpGet]
        public async Task<IActionResult> Profile() {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var name = user.Name;
            return View("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSkills([FromQuery]string id, [FromBody] List<string> skill) {
            _freelancerService.UpdateSkills(id,skill);
            return Ok();
        }
    }
}