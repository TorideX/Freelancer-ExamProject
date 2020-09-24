using Freelancer_Exam.Entities;
using Freelancer_Exam.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Services.Abstract
{
    public interface IOwnerService
    {
        bool AddProject(string userId, AddProjectViewModel project);
        List<Project> GetProjects(string userId);
    }
}
