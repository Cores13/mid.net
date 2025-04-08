namespace AbySalto.Mid.Domain.DTOs.Requests
{
    public class DimensionsRequestDto
    {
        public int? Id { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public int ProductId { get; set; }
    }
}