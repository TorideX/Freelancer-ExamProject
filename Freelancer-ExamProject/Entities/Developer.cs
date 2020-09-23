using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Entities
{
    public class Developer
    {
        public string DeveloperId { get; set; }
        public User User { get; set; }
        public short Rating { get; set; }
        public List<BidRequest> BidRequests { get; set; }
        public List<DeveloperSkill> DeveloperSkill { get; set; }
    }
}
