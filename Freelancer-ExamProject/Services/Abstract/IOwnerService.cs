using Freelancer_Exam.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Services.Abstract
{
    public interface IOwnerService
    {
        void AddProject(Project project);
        List<Project> GetProjects(string ownerId);
    }
}
