using Freelancer_Exam.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Services.Abstract
{
    public interface IFreelancerService
    {
        List<Project> GetAllProjects();
        void AddProject(Project project);
        void UpdateSkills(string dId, IEnumerable<string> skills);
    }
}
