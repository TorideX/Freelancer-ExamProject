using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Entities
{
    public class Owner
    {
        public string OwnerId { get; set; }
        public User User { get; set; }
        public List<Project> Projects { get; set; }
    }
}
