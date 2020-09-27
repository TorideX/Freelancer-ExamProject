using Freelancer_Exam.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.ViewModels
{
    public class AddProjectViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<string> RequiredSkill { get; set; }
        [Required]
        public double MinPrice { get; set; }
        [Required]
        public double MaxPrice { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}
