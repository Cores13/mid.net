namespace AbySalto.Mid.Domain.DTOs.Responses
{
    public class DimensionsResponseDto
    {
        public int? Id { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public int ProductId { get; set; }
    }
}