using AbySalto.Mid.Domain.DTOs.Requests;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.DTOs.Responses
{
    public class ApiProductResponseDto
    {
        public List<Product?> Products { get; set; }
        public int Total { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }
    }
}
