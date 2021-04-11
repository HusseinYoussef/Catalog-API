using System;
using System.Collections.Generic;
using System.Linq;
using Catalog_API.Dtos;
using Catalog_API.Extensions;
using Catalog_API.Models;
using Catalog_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            IEnumerable<ItemReadDto> items = _itemRepository.GetItems().Select(i => i.AsDto());

            if (items.Count() == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetItem(Guid id)
        {
            Item item = _itemRepository.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }

        [HttpPost]
        public IActionResult CreateItem(ItemCreateDto itemDto)
        {
            Item item = new Item()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price??0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _itemRepository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateItem(Guid id, ItemUpdateDto itemDto)
        {
            Item item = _itemRepository.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }

            item.Name = itemDto.Name;
            item.Price = itemDto.Price??0;
            item.UpdatedAt = DateTime.UtcNow;

            _itemRepository.UpdateItem(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(Guid id)
        {
            Item item = _itemRepository.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }

            _itemRepository.DeleteItem(id);
            return NoContent();
        }
    }
}