using Freelancer_Exam.Entities;
using Freelancer_Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.DTOs
{
    public class ProjectDetailsDTO
    {
        public Project Project { get; set; }
        public List<BidRequest> BidRequests { get; set; }
        public UserType UserType { get; set; }
        public string UserId { get; set; }
    }
}
