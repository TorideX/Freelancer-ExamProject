using System;
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
    [Authorize(Roles = "Owner")]
    public class OwnerController : Controller {
        private readonly UserManager<User> _userManager;
        private readonly FreelancerDbContext _freelancerDbContext;
        private readonly IOwnerService _ownerService;
        public OwnerController(UserManager<User> userManager, FreelancerDbContext freelancerDbContext, IOwnerService ownerService) {
            _userManager = userManager;
            _freelancerDbContext = freelancerDbContext;
            _ownerService = ownerService;
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
        
        [HttpPost()]
        public async Task<IActionResult> AddProject([FromRoute]string id, [FromBody] AddProjectViewModel model) {
            var a = _ownerService.AddProject(id, model);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ProjectDetails([FromQuery]string projectId) {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return null;
            var owner = _ownerService.GetOwnerById(user.Id);
            if (owner == null)
                return null;

            var project = _ownerService.GetProjects(owner.OwnerId).FirstOrDefault(p => p.ProjectId == projectId);

            if (project == null)
                return null;
            var bidRequest = _ownerService.GetAllBidRequests(owner.OwnerId)
                .Where(br => br.Project.ProjectId == project.ProjectId);

            var model = new ProjectDetailViewModel {
                Description = project.Description,
                Status = project.Status,
                Title = project.Title,
                MaxPrice = project.MaxPrice,
                MinPrice = project.MinPrice,
                ProjectId = project.ProjectId,
                RequiredSkill = project.ProjectSkill.Select(ps => new SkillViewModel {
                    Name = ps.Skill.Name,
                    SkillId = ps.Skill.SkillId
                }).ToList(),
                OwnerViewModel = new OwnerViewModel {
                  Id  = owner.OwnerId
                },
                BidRequestViewModels = bidRequest?.Select(br => new BidRequestViewModel {
                    DaysToFinish = br.DaysToFinish,
                    Note = br.Note,
                    Price = br.Price,
                    CreationDate = br.CreationDate.ToString("Y"),
                    RequestStatus = br.RequestStatus,
                    BidRequestId = br.BidRequestId,
                    DeveloperViewModel = new DeveloperViewModel {
                        Id = br.Developer.DeveloperId,
                        Rating = br.Developer.Rating,
                        User = new UserViewModel {
                            Country = br.Developer.User.Country,
                            Email = br.Developer.User.Email,
                            Name = br.Developer.User.Name,
                            Surname = br.Developer.User.Surname,
                            JoinedDate = br.Developer.User.JoinedDate.ToString("Y"),
                            ProfilePicture = br.Developer.User.ProfilePicture
                        },
                        DeveloperSkillViewModel = br.Developer.DeveloperSkill.Select(bf => new SkillViewModel {
                            Name = bf.Skill.Name,
                            SkillId = bf.Skill.SkillId
                        }).ToList()
                    }
                }).ToList()
            };
            return View("ProjectDetail",model);
        }


        public async Task<IActionResult> AcceptBidRequest(string userId,string requestId) {
            _ownerService.AcceptBidRequest(userId, requestId);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> Profile(int currentPage = 1,string filterKeyword = "All") {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return null;
            var owner = _ownerService.GetOwnerById(user.Id);
            if (owner == null)
                return null;

            int maxRows = 3;
            int count = owner.Projects.Count;
            owner.Projects = (from proj in owner.Projects select proj)
                .OrderBy(proj => proj.Status)
                .Skip((currentPage - 1) * maxRows)
                .Take(maxRows).ToList();

            var owMode = new ownerprofVM {PagingModel = new PagingModel(), Owner = owner};
            double pageCount = (double)(count / Convert.ToDecimal(maxRows));
            owMode.PagingModel.PageCount = (int)Math.Ceiling(pageCount);
            owMode.PagingModel.CurrentPageIndex = currentPage;
            
            return View(owMode);
        }
    }
}