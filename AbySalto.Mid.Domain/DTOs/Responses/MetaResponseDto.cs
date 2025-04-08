namespace AbySalto.Mid.Domain.DTOs.Responses
{
    public class MetaResponseDto
    {
        public int? Id { get; set; }
        public string Barcode { get; set; }
        public string QrCode { get; set; }
        public int ProductId { get; set; }
    }
}