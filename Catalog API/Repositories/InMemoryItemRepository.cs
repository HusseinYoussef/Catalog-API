using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Models;

namespace Catalog_API.Repositories
{
    public class InMemoryItemRepository : IItemRepository
    {
        private List<Item> _items;

        public InMemoryItemRepository()
        {
            _items = new List<Item>() {
                new Item {Id=Guid.NewGuid(), Name="T-Shirt", Price=15},
                new Item {Id=Guid.NewGuid(), Name="Pants", Price=20},
                new Item {Id=Guid.NewGuid(), Name="Jacket", Price=50},
                new Item {Id=Guid.NewGuid(), Name="Shoes", Price=10}
            };
        }

        public async Task CreateItemAsync(Item item)
        {
            _items.Add(item);
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            int idx = _items.FindIndex(i => i.Id == id);
            _items.RemoveAt(idx);
            await Task.CompletedTask;
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            Item item = _items.Where(i => i.Id == id).FirstOrDefault();
            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(string query)
        {
            if (!String.IsNullOrEmpty(query))
            { 
                return await Task.FromResult(_items.Where(i => i.Name.ToLower().Contains(query.ToLower())));
            }
            return await Task.FromResult(_items);
        }

        public async Task UpdateItemAsync(Item item)
        {
            int idx = _items.FindIndex(i => i.Id == item.Id);
            _items[idx] = item;
            await Task.CompletedTask;
        }
    }
}