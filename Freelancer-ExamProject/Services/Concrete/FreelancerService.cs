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
    public class FreelancerService : IFreelancerService
    {
        private readonly FreelancerDbContext freelancerDb;

        public FreelancerService(FreelancerDbContext freelancerDb)
        {
            this.freelancerDb = freelancerDb;
        }

        public void AddProject(Project project)
        {
            project.ProjectId = Guid.NewGuid().ToString();
            freelancerDb.Projects.Add(project);
            freelancerDb.SaveChanges();
        }

        public List<Project> GetAllProjects()
        {
            return freelancerDb.Projects
                //.Include(t=>t.Owner)
                //.Include(t=>t.RequiredSkill)
                //.Include(t=>t.Owner.User.Country)
                .ToList();
        }
    }
}
