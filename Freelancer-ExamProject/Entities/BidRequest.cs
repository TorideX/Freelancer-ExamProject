using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Entities
{
    public class BidRequest
    {
        public string BidRequestId { get; set; }
        public Developer Developer { get; set; }
        public Project Project { get; set; }
        public double Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string Note { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime Deadline { get; set; }
    }
}
