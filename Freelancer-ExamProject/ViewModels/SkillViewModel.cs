using System.ComponentModel.DataAnnotations;

namespace Freelancer_Exam.ViewModels {
    public class SkillViewModel {
        [Required]
        public string SkillId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}