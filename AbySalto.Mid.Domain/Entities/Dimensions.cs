﻿using AbySalto.Mid.Domain.Primitives;

namespace AbySalto.Mid.Domain.Entities
{
    public class Dimensions : Entity
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}