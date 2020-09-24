namespace Freelancer_Exam.ViewModels {
    public class BidRequestViewModel {
        public string BidRequestId { get; set; }
        public DeveloperViewModel DeveloperViewModel { get; set; }
        public ProjectViewModel ProjectViewModel { get; set; }
        public double Price { get; set; }
        public string CreationDate { get; set; }
        public string Note { get; set; }
        public bool IsConfirmed { get; set; }
        public string Deadline { get; set; }
    }
}