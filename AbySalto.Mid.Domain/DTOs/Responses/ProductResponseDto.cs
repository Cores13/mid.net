namespace AbySalto.Mid.Domain.DTOs.Responses
{
    public class ProductResponseDto
    {
        public int? Id { get; set; }
        public int? ApiId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public double DiscountPercentage { get; set; }
        public double Rating { get; set; }
        public int Stock { get; set; }
        public List<string> Tags { get; set; }
        public string? Brand { get; set; }
        public string Sku { get; set; }
        public int Weight { get; set; }
        public DimensionsResponseDto Dimensions { get; set; }
        public string WarrantyInformation { get; set; }
        public string ShippingInformation { get; set; }
        public string AvailabilityStatus { get; set; }
        public virtual ICollection<ReviewResponseDto> Reviews { get; set; }
        public string ReturnPolicy { get; set; }
        public int MinimumOrderQuantity { get; set; }
        public MetaResponseDto Meta { get; set; }
        public string Thumbnail { get; set; }
        public List<string> Images { get; set; }
    }
}
