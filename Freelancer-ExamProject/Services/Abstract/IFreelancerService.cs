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
        void UpdateSkills(string dId, IEnumerable<string> skills);
        bool CreateBidRequest(string userId, CreateBidRequestViewModel requestModel);
        bool CompleteProject(string userId, string requestId);
    }
}
