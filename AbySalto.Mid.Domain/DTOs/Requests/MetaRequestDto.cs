namespace AbySalto.Mid.Domain.DTOs.Requests
{
    public class MetaRequestDto
    {
        public int? Id { get; set; }
        public string Barcode { get; set; }
        public string QrCode { get; set; }
        public int ProductId { get; set; }
    }
}