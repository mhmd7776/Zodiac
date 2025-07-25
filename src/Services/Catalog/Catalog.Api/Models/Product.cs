﻿namespace Catalog.Api.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<string> Categories { get; set; } = [];
    }
}
