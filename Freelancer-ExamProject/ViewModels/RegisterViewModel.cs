using System.ComponentModel.DataAnnotations;

namespace Freelancer_Exam.ViewModels {
    public enum Job {
        Owner,
        Freelancer
    }

    public class RegisterViewModel {
        [EmailAddress] [Required]
        public string Email { get; set; }
        [Required] 
        public string FirstName { get; set; }
        [Required] 
        public string LastName { get; set; }
        
        [Required]
        public string Country { get; set; }
        
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public Job Job { get; set; }
    }
}