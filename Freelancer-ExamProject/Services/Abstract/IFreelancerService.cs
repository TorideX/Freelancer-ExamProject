using Freelancer_Exam.Entities;
using Freelancer_Exam.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Services.Abstract
{
    public interface IFreelancerService
    {
        void RemoveSkill(string dId, string skillName);
        void AddSkill(string dId, string skillName);
        bool CreateBidRequest(string userId, CreateBidRequestViewModel requestModel);
        bool CompleteProject(string userId, string requestId);
    }
}
