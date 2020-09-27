using Freelancer_Exam.DTOs;
using Freelancer_Exam.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Services.Abstract
{
    public interface IHomeService
    {
        List<Project> GetAllProjects();
        ProjectDetailsDTO GetProjectById(string projectId);
    }
}
