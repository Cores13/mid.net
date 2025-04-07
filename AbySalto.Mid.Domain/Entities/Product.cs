using AbySalto.Mid.Domain.Enums;
using AbySalto.Mid.Domain.Primitives;
using System.Text.Json.Serialization;

namespace AbySalto.Mid.Domain.Entities
{
    public class Product : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public double DiscountPercentage { get; set; }
        public double Rating { get; set; }
        public int Stock { get; set; }
        public List<string> Tags { get; set; }
        public string Brand { get; set; }
        public string Sku { get; set; }
        public int Weight { get; set; }
        public int DimensionsId { get; set; }
        public Dimensions Dimensions { get; set; }
        public string WarrantyInformation { get; set; }
        public string ShippingInformation { get; set; }
        public string AvailabilityStatus { get; set; }
        public int ReviewId { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public string ReturnPolicy { get; set; }
        public int MinimumOrderQuantity { get; set; }
        public int MetaId { get; set; }
        public Meta Meta { get; set; }
        public string Thumbnail { get; set; }
        public List<string> Images { get; set; }
        public virtual CartProduct Cart { get; set; }
    }
}