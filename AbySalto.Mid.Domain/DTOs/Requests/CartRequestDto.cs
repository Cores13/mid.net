namespace AbySalto.Mid.Domain.DTOs.Requests
{
    public class CartRequestDto
    {
        public int Id { get; set; }
        public List<CartProductRequestDto> Products { get; set; }
        public double Total { get; set; }
        public double DiscountedTotal { get; set; }
        public int UserId { get; set; }
        public int TotalProducts { get; set; }
        public int TotalQuantity { get; set; }
    }
}
