using System.Collections.Generic;
using Freelancer_Exam.Entities;

namespace Freelancer_Exam.ViewModels {
    public class DeveloperViewModel {
        public string Id { get; set; }
        public UserViewModel User { get; set; }
        public short Rating { get; set; }
        public List<SkillViewModel> DeveloperSkillViewModel { get; set; }
        public List<BidRequestViewModel> BidRequests { get; set; }
    }
}