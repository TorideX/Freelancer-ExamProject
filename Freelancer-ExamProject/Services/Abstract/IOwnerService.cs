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
        List<Project> GetProjects(string ownerId);
        Owner GetOwnerById(string uId);
        bool AcceptBidRequest(string userId, string requestId);
        List<BidRequest> GetAllBidRequests(string ownerId);
        List<BidRequest> GetBidRequestsByProject(string userId, string projectId);
        List<ConfirmedRequest> GetConfirmedRequests(string userId);
        bool RateDeveloper(string userId, string requestId, short rating);
    }
}
