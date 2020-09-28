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

        public async Task<IActionResult> CompleteProject(string userId, string requestId) {
            _freelancerService.CompleteProject(userId, requestId);
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> Profile(int currentPage = 1) {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var developer = _freelancerService.GetDeveloperByUserId(user.Id);

            int maxRows = 3;
            int count = developer.BidRequests.Count;
            
            developer.BidRequests =  (from bidRequest in developer.BidRequests select bidRequest)
                .OrderBy(bidRequest => bidRequest.RequestStatus)
                .Skip((currentPage - 1) * maxRows)
                .Take(maxRows).ToList();

            var devModel = new devProfVm() {PagingModel = new PagingModel(), Developer = developer};
            double pageCount = (double)(count / Convert.ToDecimal(maxRows));
            devModel.PagingModel.PageCount = (int)Math.Ceiling(pageCount);
            devModel.PagingModel.CurrentPageIndex = currentPage;
            return View("Profile",devModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddBidRequest([FromBody] CreateBidRequestViewModel requestModel)
        {
            try
            {
                var response = _freelancerService.CreateBidRequest(requestModel);
            }
            catch { }
            return RedirectToAction("ProjectDetails", "Home", new { projectId = requestModel.ProjectId });
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill([FromBody] EditDeveloperSkillViewModel model) {
            _freelancerService.AddSkill(model.DeveloperId,model.SkillName);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveSkill([FromBody] EditDeveloperSkillViewModel model) {
            _freelancerService.RemoveSkill(model.DeveloperId, model.SkillName);
            return Ok();
        }
    }
}