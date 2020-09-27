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
        void RemoveSkill(string dId, string skillName);
        void AddSkill(string dId, string skillName);
    }
}
