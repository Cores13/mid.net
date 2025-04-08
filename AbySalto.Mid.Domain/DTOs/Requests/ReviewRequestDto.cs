namespace AbySalto.Mid.Domain.DTOs.Requests
{
    public class ReviewRequestDto
    {
        public int? Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set; }
        public int ProductId { get; set; }
    }
}