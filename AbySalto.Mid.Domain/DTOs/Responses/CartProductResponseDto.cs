using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.DTOs.Responses
{
    public class CartProductResponseDto
    {
        public int? Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public double DiscountPercentage { get; set; }
        public double DiscountTotal { get; set; }
        public string Thumbnail { get; set; }
    }
}
