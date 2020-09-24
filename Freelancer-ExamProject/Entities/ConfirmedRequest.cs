using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Entities
{
    public class ConfirmedRequest
    {
        public string ConfirmedRequestId { get; set; }
        public BidRequest BidRequest { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public short? Rating { get; set; }
    }
}
