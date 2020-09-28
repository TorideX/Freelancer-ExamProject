using System.Collections.Generic;
using Freelancer_Exam.Entities;

namespace Freelancer_Exam.ViewModels {
    public class ProjectDetailViewModel {
        public string ProjectId { get; set; }
        public OwnerViewModel OwnerViewModel { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<SkillViewModel> RequiredSkill { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public Status Status { get; set; }
        public List<BidRequestViewModel> BidRequestViewModels { get; set; }
    }
}