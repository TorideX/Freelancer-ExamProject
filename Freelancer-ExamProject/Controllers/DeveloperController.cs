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
using Microsoft.EntityFrameworkCore;

namespace Freelancer_Exam.Controllers {
    [Authorize(Roles = "Developer")]
    public class DeveloperController : Controller {
        private readonly IFreelancerService _freelancerService;
        private readonly UserManager<User> _userManager;
        private readonly FreelancerDbContext _freelancerDb;

        public DeveloperController(IFreelancerService freelancerService, UserManager<User> userManager, FreelancerDbContext freelancerDb) {
            _freelancerService = freelancerService;
            _userManager = userManager;
            _freelancerDb = freelancerDb;
        }
        [HttpPost]
        public async Task<IActionResult> UploadProfilePhoto(IFormFile file) {
            var current = Directory.GetCurrentDirectory();
            var imgSrc = Path.Combine(current, "wwwroot", "img", file.FileName);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            currentUser.ProfilePicture = $"~/img/{file.FileName}";
            await _freelancerDb.SaveChangesAsync();
            await using var stream = new FileStream(imgSrc, FileMode.Create);
            await file.CopyToAsync(stream);
            return RedirectToAction("Profile", "Developer");
        }
        
        [HttpGet]
        public async Task<IActionResult> Profile() {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var developer = _freelancerDb.Developers?
                .Include(d => d.DeveloperSkill).ThenInclude(ds => ds.Skill).ThenInclude(s => s.DeveloperSkill)
                .Include(d => d.BidRequests).ThenInclude(br => br.Project).ThenInclude(p => p.Owner)
                .FirstOrDefault(d => d.User.Id == user.Id);
            if (developer?.DeveloperSkill != null) {
                var developerSkillList = developer.DeveloperSkill?
                    .Select(developerSkill => new DeveloperSkillViewModel {
                        DeveloperId = developerSkill.DeveloperId,
                        SkillId = developerSkill.SkillId,
                        DeveloperViewModel = new DeveloperViewModel {
                            Id = developerSkill.Developer.DeveloperId, Rating = developerSkill.Developer.Rating,
                            User = new UserViewModel {
                                JoinedDate = developerSkill.Developer.User.JoinedDate.ToString("Y"),
                                Email = developerSkill.Developer.User.Email,
                                Country = developerSkill.Developer.User.Country, Name = developerSkill.Developer.User.Name,
                                Surname = developerSkill.Developer.User.Surname,
                                ProfilePicture = developerSkill.Developer.User.ProfilePicture
                            }
                        },
                        SkillViewModel = new SkillViewModel {
                            Name = developerSkill.Skill.Name,
                            SkillId = developerSkill.Skill.SkillId
                        }
                    }).ToList();

                var developerViewModel = new DeveloperViewModel {
                    Id = developer.DeveloperId,
                    Rating = developer.Rating,
                    User = new UserViewModel {
                        JoinedDate = developer.User.JoinedDate.ToString("Y"),
                        Email = developer.User.Email,
                        Country = developer.User.Country,
                        Name = developer.User.Name,
                        Surname = developer.User.Surname,
                        ProfilePicture = developer.User.ProfilePicture
                    },
                    DeveloperSkillViewModel = developerSkillList
                };


                return View("Profile",developerViewModel);
            }
            return View("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSkills([FromQuery]string id, [FromBody] List<string> skill) {
            _freelancerService.UpdateSkills(id,skill);
            return Ok();
        }
    }
}