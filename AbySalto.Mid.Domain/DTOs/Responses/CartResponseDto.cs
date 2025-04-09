namespace AbySalto.Mid.Domain.DTOs.Responses
{
    public class CartResponseDto
    {
        public int? Id { get; set; }
        public ICollection<CartProductResponseDto?> Products { get; set; }
        public double Total { get; set; }
        public double DiscountedTotal { get; set; }
        public int UserId { get; set; }
        public int TotalProducts { get; set; }
        public int TotalQuantity { get; set; }
    }
}
