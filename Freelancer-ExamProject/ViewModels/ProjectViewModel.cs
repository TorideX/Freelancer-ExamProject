using Freelancer_Exam.Entities;

namespace Freelancer_Exam.ViewModels {
    public class ProjectViewModel {
        public string ProjectId { get; set; }
        public OwnerViewModel OwnerViewModel { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SkillViewModel RequiredSkill { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public Status Status { get; set; }
    }
}