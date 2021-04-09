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
            IEnumerable<ItemReadDto> items = _itemRepository.GetItems().Select(i => i.ItemToDto());

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
            return Ok(item.ItemToDto());
        }
    }
}