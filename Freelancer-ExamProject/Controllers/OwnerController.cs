using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Freelancer_Exam.Entities;
using Freelancer_Exam.Entities.Db_Context;
using Freelancer_Exam.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freelancer_Exam.Controllers {
    public class OwnerController : Controller {
        private readonly UserManager<User> _userManager;
        private readonly FreelancerDbContext _freelancerDbContext;
        public OwnerController(UserManager<User> userManager, FreelancerDbContext freelancerDbContext) {
            _userManager = userManager;
            _freelancerDbContext = freelancerDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfilePhoto(IFormFile file) {
            var current = Directory.GetCurrentDirectory();
            var imgSrc = Path.Combine(current, "wwwroot", "img", file.FileName);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            currentUser.ProfilePicture = $"~/img/{file.FileName}";
            await _freelancerDbContext.SaveChangesAsync();
            await using var stream = new FileStream(imgSrc, FileMode.Create);
            await file.CopyToAsync(stream);
            return RedirectToAction("Profile", "Owner");
        }


        [HttpGet]
        public async Task<IActionResult> Profile() {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var owner = await _freelancerDbContext.Owners?
                .Include(o => o.Projects)
                .FirstOrDefaultAsync(o => o.User.Id == user.Id);
            var projectList = new List<ProjectViewModel>();
            if (owner.Projects != null) {
                foreach (var proj in owner.Projects) {
                    projectList.Add(new ProjectViewModel {
                        RequiredSkill = new SkillViewModel {
                            Name = proj.RequiredSkill.Name,
                            SkillId = proj.RequiredSkill.SkillId
                        },
                        Description = proj.Description,
                        MaxPrice = proj.MaxPrice,
                        MinPrice = proj.MinPrice,
                        ProjectId = proj.ProjectId,
                        Status = proj.Status,
                        Title = proj.Title
                    });
                }
            }

            var ownerViewModel = new OwnerViewModel {
                User = new UserViewModel {
                    ProfilePicture = owner.User.ProfilePicture,
                    JoinedDate = owner.User.JoinedDate.ToString("Y"),
                    Country = owner.User.Country,
                    Email = owner.User.Email,
                    Name = owner.User.Name,
                    Surname = owner.User.Surname
                },
                Project = projectList
            };
            return View(ownerViewModel);
        }
    }
}