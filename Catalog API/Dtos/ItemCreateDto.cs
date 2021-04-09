using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog_API.Dtos
{
    public class ItemCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 1000)]
        public decimal Price { get; set; } 
    }
}