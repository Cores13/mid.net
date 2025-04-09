using AbySalto.Mid.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbySalto.Mid.Domain.Entities
{
    public class UserFavorite : Entity
    {
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}