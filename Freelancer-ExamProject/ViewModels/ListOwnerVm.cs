using Freelancer_Exam.Entities;

namespace Freelancer_Exam.ViewModels {
    public class PagingModel {
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
    
    public class ownerprofVM {
        public Owner Owner { get; set; }
        public string FilterKeyword { get; set; }
        public PagingModel PagingModel { get; set; }
    }
}