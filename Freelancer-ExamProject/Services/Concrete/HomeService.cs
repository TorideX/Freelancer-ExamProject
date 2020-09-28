using Freelancer_Exam.DTOs;
using Freelancer_Exam.Entities;
using Freelancer_Exam.Entities.Db_Context;
using Freelancer_Exam.Models;
using Freelancer_Exam.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Services.Concrete
{
    public class HomeService : IHomeService
    {
        private readonly FreelancerDbContext freelancerDb;
        public HomeService(FreelancerDbContext freelancerDb)
        {
            this.freelancerDb = freelancerDb;
        }

        public List<Project> GetAllProjects()
        {
            return freelancerDb.Projects
                .Include(t => t.Owner.User)
                .Include(t => t.ProjectSkill)
                .ThenInclude(ps => ps.Skill)
                .ToList();
        }

        public List<Project> GetProjectsBySearch(string search)
        {
            return freelancerDb.Projects
                .Include(t => t.Owner.User)
                .Include(t => t.ProjectSkill)
                .ThenInclude(ps => ps.Skill)
                .Where(t=>t.Title.Contains(search) || 
                    t.Owner.User.Name.Contains(search) || 
                    t.Owner.User.Surname.Contains(search) || 
                    t.ProjectSkill.Any(i=>i.Skill.Name.Contains(search)))
                .ToList();
        }

        public UserType GetUserType(string userId)
        {
            var dev = freelancerDb.Developers.FirstOrDefault(t => t.DeveloperId == userId);
            if (dev != null) return UserType.Developer;

            var owner = freelancerDb.Owners.FirstOrDefault(t => t.OwnerId == userId);
            if (owner != null) return UserType.Owner;

            return UserType.NotAuthorized;
        }
        public ProjectDetailsDTO GetProjectById(string projectId)
        {
            var project = freelancerDb.Projects
                .Include(t => t.Owner.User)
                .Include(t => t.ProjectSkill)
                .ThenInclude(ps => ps.Skill)
                .FirstOrDefault(t => t.ProjectId == projectId);

            var bidRequests = freelancerDb.BidRequests
                .Include(t=>t.Project.Owner.User)
                .Include(t => t.Project.ProjectSkill).ThenInclude(t=>t.Skill)
                .Include(t=>t.Developer.DeveloperSkill).ThenInclude(t=>t.Skill)
                .Include(t=>t.Developer.User)
                .Where(t => t.Project == project).ToList();

            if (project == null || bidRequests == null) return null;

            var projectDetailsDTO = new ProjectDetailsDTO
            {
                Project = project,
                BidRequests = bidRequests
            };
            return projectDetailsDTO;
        }
    }
}
