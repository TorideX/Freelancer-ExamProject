using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Entities
{
    public enum RequestStatus
    {
        Rejected = 0,
        Pending,
        Confirmed
    }
    public class BidRequest
    {
        public string BidRequestId { get; set; }
        public Developer Developer { get; set; }
        public Project Project { get; set; }
        public double Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string Note { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public int DaysToFinish { get; set; }        
    }
}
