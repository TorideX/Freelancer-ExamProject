using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Entities
{
    public class Project
    {
        public string ProjectId { get; set; }
        public Owner Owner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual Skill RequiredSkill { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public Status Status { get; set; }
    }
}
