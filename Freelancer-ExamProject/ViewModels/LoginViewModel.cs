using System.ComponentModel.DataAnnotations;

namespace Freelancer_Exam.ViewModels {
    public class LoginViewModel {
        [Required] [EmailAddress] 
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}