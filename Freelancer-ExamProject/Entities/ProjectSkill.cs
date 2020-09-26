namespace Freelancer_Exam.Entities {
    public class ProjectSkill {
        public string ProjectId { get; set; }
        public string SkillId { get; set; }
        public Skill Skill { get; set; }
        public Project Project { get; set; }
    }
}