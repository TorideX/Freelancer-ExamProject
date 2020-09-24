using System.Collections.Generic;
using Freelancer_Exam.Entities;

namespace Freelancer_Exam.ViewModels {
    public class DeveloperViewModel {
        public string Id { get; set; }
        public UserViewModel User { get; set; }
        public short Rating { get; set; }
        public List<DeveloperSkillViewModel> DeveloperSkillViewModel { get; set; }
        public List<BidRequest> BidRequests { get; set; }
    }
}