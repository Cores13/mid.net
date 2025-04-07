using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Enums;
using AbySalto.Mid.Domain.Primitives;
using System.Text.Json.Serialization;

namespace AbySalto.Mid.Domain.Entities
{
    public class Cart : Entity
    {
        public virtual ICollection<CartProduct?> Products { get; set; }
        public double Total { get; set; }
        public double DiscountedTotal { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TotalProducts { get; set; }
        public int TotalQuantity { get; set; }
    }
}