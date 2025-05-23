﻿using AbySalto.Mid.Domain.Primitives;

namespace AbySalto.Mid.Domain.Entities
{
    public class Cart : Entity
    {
        public double Total { get; set; }
        public double DiscountedTotal { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TotalProducts { get; set; }
        public int TotalQuantity { get; set; }
        public virtual ICollection<CartProduct?> Products { get; set; }
    }
}