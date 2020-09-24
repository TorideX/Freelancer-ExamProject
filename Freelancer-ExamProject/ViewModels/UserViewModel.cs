using System.ComponentModel.DataAnnotations;

namespace Freelancer_Exam.ViewModels {
    public class UserViewModel {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Country { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public string JoinedDate { get; set; }
    }
}