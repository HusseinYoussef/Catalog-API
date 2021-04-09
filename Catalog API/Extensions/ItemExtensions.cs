using System;
using Catalog_API.Dtos;
using Catalog_API.Models;

namespace Catalog_API.Extensions
{
    public static class ItemExtensions
    {
        public static ItemReadDto ItemToDto(this Item item)
        {
            return new ItemReadDto()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price
            };
        }
    }
}