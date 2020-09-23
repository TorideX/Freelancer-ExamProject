using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Entities
{
    public class Skill
    {
        public string SkillId { get; set; }
        public string Name { get; set; }
        public List<DeveloperSkill> DeveloperSkill { get; set; }
    }
}
