using AbySalto.Mid.Domain.Enums;
using AbySalto.Mid.Domain.Primitives;
using System.Text.Json.Serialization;

namespace AbySalto.Mid.Domain.Entities
{
    public class Review : Entity
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}