using Freelancer_Exam.Entities;
using Freelancer_Exam.Entities.Db_Context;
using Freelancer_Exam.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Freelancer_Exam.ViewModels;

namespace Freelancer_Exam.Services.Concrete
{
    public class FreelancerService : IFreelancerService
    {
        private readonly FreelancerDbContext freelancerDb;

        public FreelancerService(FreelancerDbContext freelancerDb)
        {
            this.freelancerDb = freelancerDb;
        }

        public bool CreateBidRequest(string userId, CreateBidRequestViewModel requestModel)
        {
            var dev = freelancerDb.Developers.FirstOrDefault(t => t.DeveloperId == userId);
            var project = freelancerDb.Projects.FirstOrDefault(t => t.ProjectId == requestModel.ProjectId);
            if (dev == null || project == null) return false;

            var bidRequest = new BidRequest
            {
                BidRequestId = Guid.NewGuid().ToString(),
                CreationDate = DateTime.Now,
                Developer = dev,
                Project = project,
                Note = requestModel.Note,
                Price = requestModel.Price,
                DaysToFinish = requestModel.DaysToFinish,
                RequestStatus = RequestStatus.Pending
            };
            freelancerDb.BidRequests.Add(bidRequest);
            freelancerDb.SaveChanges();
            return true;
        }

        public bool CompleteProject(string userId, string requestId)
        {
            var dev = freelancerDb.Developers.FirstOrDefault(t => t.DeveloperId == userId);
            var confirmedRequest = freelancerDb.ConfirmedRequests
                .Include(t => t.BidRequest.Project)
                .Include(t => t.BidRequest.Developer)
                .FirstOrDefault(t => t.ConfirmedRequestId == requestId);
            if (dev == null || confirmedRequest == null) return false;
            if (confirmedRequest.BidRequest.Developer != dev) return false;

            confirmedRequest.BidRequest.Project.Status = Status.Completed;
            freelancerDb.SaveChanges();
            return false;
        }

        public void UpdateSkills(string dId, IEnumerable<string> skillNames) {
            // var developer = freelancerDb.Developers.FirstOrDefault(d => d.DeveloperId == dId);
            // if(developer == null)
            //     return;
            //
            //
            // var skills = freelancerDb.Skills ?? new InternalDbSet<Skill>(freelancerDb);
            // foreach (var skillName in skillNames) {
            //     var existSkill = skills.FirstOrDefault(s => s.Name == skillName);
            //     if (existSkill!= null) {
            //         existSkill = skills.Add(new Skill {Name = skillName}).Entity;
            //     }
            //
            //     if (developer.Skills.FirstOrDefault(s => s.Name == skillName) == null) {
            //         developer.Skills.Add(existSkill);
            //     }
            // }
        }
        
        public List<Project> GetAllProjects()
        {
            return freelancerDb.Projects
                .Include(t => t.Owner)
                .Include(t => t.RequiredSkill)
                //.Include(t => t.Owner.User.Country)
                .ToList();
        }
    }
}
