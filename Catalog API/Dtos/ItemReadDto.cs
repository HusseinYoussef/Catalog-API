using System;

namespace Catalog_API.Dtos
{
    public class ItemReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}