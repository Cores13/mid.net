using AbySalto.Mid.Domain.Primitives;

namespace AbySalto.Mid.Domain.Entities
{
    public class Meta : Entity
    {
        public string Barcode { get; set; }
        public string QrCode { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}