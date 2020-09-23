using System;

namespace Freelancer_Exam.Entities {
    public class DeveloperSkill {
        public string DeveloperId { get; set; }
        public string SkillId { get; set; }
        public Skill Skill { get; set; }
        public Developer Developer { get; set; }
    }
}