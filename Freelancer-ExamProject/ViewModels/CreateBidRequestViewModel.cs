using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.ViewModels
{
    public class CreateBidRequestViewModel
    {
        public string ProjectId { get; set; }
        public double Price { get; set; }
        public string Note { get; set; }
        public int DaysToFinish { get; set; }
    }
}
