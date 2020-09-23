using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Freelancer_Exam.Entities;
using Freelancer_Exam.Services.Abstract;
using Freelancer_Exam.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freelancer_Exam.Controllers {
    public class DeveloperController : Controller {
        private readonly IFreelancerService _freelancerService;

        public DeveloperController(IFreelancerService freelancerService) {
            _freelancerService = freelancerService;
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
            var devModel = new DeveloperViewModel {
                Id = "5",
                Rating = 1,
                Skills = new List<SkillViewModel> {
                    new SkillViewModel {
                        Name = "Javascript",
                        SkillId = "1"
                    },
                    new SkillViewModel {
                        Name = "Python",
                        SkillId = "2"
                    },
                    new SkillViewModel {
                        Name = "Ruby",
                        SkillId = "3"
                    },
                    new SkillViewModel {
                        Name = "Go",
                        SkillId = "4"
                    },
                    new SkillViewModel {
                        Name = "Kotlin",
                        SkillId = "5"
                    },
                    new SkillViewModel {
                        Name = "Swift",
                        SkillId = "6"
                    },
                    new SkillViewModel {
                        Name = "Php",
                        SkillId = "7"
                    },
                    new SkillViewModel {
                        Name = "Devops",
                        SkillId = "8"
                    }
                }
            };
            return View("Profile", devModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSkills([FromQuery]string id, [FromBody] List<string> skill) {
            _freelancerService.UpdateSkills(id,skill);
            return Ok();
        }
    }
}