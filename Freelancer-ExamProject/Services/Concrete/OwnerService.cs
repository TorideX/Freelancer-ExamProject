using Freelancer_Exam.Entities;
using Freelancer_Exam.Entities.Db_Context;
using Freelancer_Exam.Services.Abstract;
using Freelancer_Exam.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Services.Concrete
{
    public class OwnerService : IOwnerService
    {
        private readonly FreelancerDbContext freelancerDb;

        public OwnerService(FreelancerDbContext freelancerDb)
        {
            this.freelancerDb = freelancerDb;
        }

        public List<Project> GetProjects(string ownerId)
        {
            var owner = freelancerDb.Owners
                .Include(t=>t.User)
                .Include(t=>t.Projects)
                .ThenInclude(pr=>pr.ProjectSkill)
                .FirstOrDefault(t => t.OwnerId == ownerId);

            if (owner == null) return new List<Project>();
            return owner.Projects;
        }

        private List<Skill> GetSkillsBySkillNames(List<string> skillNames)
        {
            var skills = new List<Skill>();
            foreach (var item in skillNames)
            {
                var skill = freelancerDb.Skills.FirstOrDefault(t => t.Name == item);
                if (skill == null)
                {
                    skill = new Skill { SkillId = Guid.NewGuid().ToString(), Name = item };
                }
                skills.Add(skill);
            }
            return skills;
        }
        private List<ProjectSkill> AddProjectSkillsBySkills(List<Skill> skills, Project project)
        {
            var projectSkills = new List<ProjectSkill>();
            foreach (var item in skills)
            {
                var projectSkill = new ProjectSkill
                {
                    ProjectSkillId = Guid.NewGuid().ToString(),
                    Project = project,
                    Skill = item
                };
                projectSkills.Add(projectSkill);
            }
            return projectSkills;
        }

        public bool AddProject(string userId, AddProjectViewModel project)
        {
            var owner = freelancerDb.Owners.FirstOrDefault(t => t.User.Id == userId);
            if (owner == null) return false;

            var requiredSkills = GetSkillsBySkillNames(project.RequiredSkill);            

            var newProject = new Project
            {
                ProjectId = Guid.NewGuid().ToString(),
                Owner = owner,
                Description = project.Description,
                Title = project.Title,
                MaxPrice = project.MaxPrice,
                MinPrice = project.MinPrice,
                Status = project.Status
            };
            newProject.ProjectSkill = AddProjectSkillsBySkills(requiredSkills, newProject);

            freelancerDb.Projects.Add(newProject);
            freelancerDb.SaveChanges();
            return true;
        }
        public bool AcceptBidRequest(string userId, string requestId)
        {
            var owner = freelancerDb.Owners.FirstOrDefault(t => t.OwnerId == userId);
            var bidRequest = freelancerDb.BidRequests
                .Include(t => t.Project.Owner)
                .FirstOrDefault(t => t.BidRequestId == requestId);
            if (owner == null || bidRequest == null) return false;

            if (bidRequest.Project.Owner != owner) return false;
            bidRequest.RequestStatus = RequestStatus.Confirmed;
            bidRequest.Project.Status = Status.Working;

            var confirmedRequest = new ConfirmedRequest
            {
                ConfirmedRequestId = Guid.NewGuid().ToString(),
                BidRequest = bidRequest,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(bidRequest.DaysToFinish)
            };
            freelancerDb.ConfirmedRequests.Add(confirmedRequest);
            freelancerDb.SaveChanges();
            return true;
        }

        public List<BidRequest> GetAllBidRequests(string userId)
        {
            var bidRequests = freelancerDb.BidRequests
                .Include(t => t.Project.Owner)
                .Where(t => t.Project.Owner.OwnerId == userId).ToList();
            return bidRequests;
        }

        public List<BidRequest> GetBidRequestsByProject(string userId, string projectId)
        {
            var bidRequests = freelancerDb.BidRequests
                .Include(t => t.Project.Owner)
                .Where(t => t.Project.ProjectId == projectId && t.Project.Owner.OwnerId == userId)
                .ToList();
            return bidRequests;
        }

        public List<ConfirmedRequest> GetConfirmedRequests(string userId)
        {
            var owner = freelancerDb.Owners.FirstOrDefault(t => t.OwnerId == userId);
            if (owner == null) return null;

            var confirmedRequests = freelancerDb.ConfirmedRequests
                .Include(t => t.BidRequest.Project.Owner)
                .Where(t => t.BidRequest.Project.Owner == owner).ToList();
            return confirmedRequests;
        }

        public bool RateDeveloper(string userId, string requestId, short rating)
        {
            var owner = freelancerDb.Owners.FirstOrDefault(t => t.OwnerId == userId);
            var confirmedRequest = freelancerDb.ConfirmedRequests
                .Include(t => t.BidRequest.Developer)
                .Include(t => t.BidRequest.Project.Owner)
                .FirstOrDefault(t => t.ConfirmedRequestId == requestId);
            if (owner == null || confirmedRequest == null) return false;

            if (confirmedRequest.BidRequest.Project.Owner != owner ||
                confirmedRequest.BidRequest.Project.Status!= Status.Completed ||
                confirmedRequest.Rating != null) return false;

            confirmedRequest.Rating = rating;
            confirmedRequest.BidRequest.Developer.RatingCount++;
            confirmedRequest.BidRequest.Developer.Rating = Convert.ToInt16((confirmedRequest.BidRequest.Developer.Rating + rating) / confirmedRequest.BidRequest.Developer.RatingCount);
            freelancerDb.SaveChanges();
            return true;
        }
    }
}
