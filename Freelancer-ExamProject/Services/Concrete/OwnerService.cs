using Freelancer_Exam.Entities;
using Freelancer_Exam.Entities.Db_Context;
using Freelancer_Exam.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                .Include(t=>t.Projects)
                .ThenInclude(pr=>pr.RequiredSkill)
                .FirstOrDefault(t => t.OwnerId == ownerId);

            if (owner == null) return new List<Project>();
            return owner.Projects;
        }
        public void AddProject(Project project)
        {
            project.ProjectId = Guid.NewGuid().ToString();
            freelancerDb.Projects.Add(project);
            freelancerDb.SaveChanges();
        }


    }
}
