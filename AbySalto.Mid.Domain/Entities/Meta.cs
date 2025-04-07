using AbySalto.Mid.Domain.Enums;
using AbySalto.Mid.Domain.Primitives;
using System.Text.Json.Serialization;

namespace AbySalto.Mid.Domain.Entities
{
    public class Meta : Entity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Barcode { get; set; }
        public string QrCode { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}