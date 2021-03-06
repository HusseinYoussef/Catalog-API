using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Dtos;
using Catalog_API.Extensions;
using Catalog_API.Models;
using Catalog_API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ItemReadDto>>> GetItems(string query=null)
        {
            IEnumerable<ItemReadDto> items = (await _itemRepository.GetItemsAsync(query)).Select(i => i.AsDto()).ToList();

            if (items.Count() == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemReadDto>> GetItem(Guid id)
        {
            Item item = await _itemRepository.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ItemReadDto>> CreateItem([FromBody] ItemCreateDto itemDto)
        {
            Item item = new Item()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price??0,
                UserId = HttpContext.GetUserId(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _itemRepository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateItem([FromRoute] Guid id, [FromBody] ItemUpdateDto itemDto)
        {
            Item item = await _itemRepository.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            if (!CheckItemOwner(item))
            {
                return BadRequest(new { Errors = "You aren't the owner of this post." });
            }

            item.Name = itemDto.Name;
            item.Price = itemDto.Price??0;
            item.UpdatedAt = DateTime.UtcNow;

            await _itemRepository.UpdateItemAsync(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            Item item = await _itemRepository.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            if (!CheckItemOwner(item))
            {
                return BadRequest(new { Errors = "You aren't the owner of this post." });
            }

            await _itemRepository.DeleteItemAsync(id);
            return NoContent();
        }

        private bool CheckItemOwner(Item item)
        {
            return item.UserId == HttpContext.GetUserId();
        }
    }
}