using Freelancer_Exam.Entities;
using Freelancer_Exam.Entities.Db_Context;
using Freelancer_Exam.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

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
                .Include(t => t.Owner.User.Country)
                .ToList();
        }
    }
}
