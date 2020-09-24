using System.Collections.Generic;
using Freelancer_Exam.Entities;

namespace Freelancer_Exam.ViewModels {
    public class OwnerViewModel {
        public string Id { get; set; }
        public UserViewModel User { get; set; }
        public List<ProjectViewModel> Project { get; set; }
        public AddProjectViewModel AddProjectViewModel { get; set; }
    }
}