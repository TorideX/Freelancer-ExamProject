using System.Collections.Generic;
using Freelancer_Exam.Entities;

namespace Freelancer_Exam.ViewModels {
    public class ListProjectVm {
        public List<Project> Project { get; set; }
        public PagingModel PagingModel { get; set; }
        public string SearchKeyword { get; set; }
    }
}