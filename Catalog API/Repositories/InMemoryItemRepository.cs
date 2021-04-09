using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CreateItem(Item item)
        {
            _items.Add(item);
        }

        public void DeleteItem(Guid id)
        {
            int idx = _items.FindIndex(i => i.Id == id);
            _items.RemoveAt(idx);
        }

        public Item GetItem(Guid id)
        {
            Item item = _items.Where(i => i.Id == id).FirstOrDefault();
            return item;
        }

        public IEnumerable<Item> GetItems()
        {
            return _items;
        }

        public void UpdateItem(Item item)
        {
            int idx = _items.FindIndex(i => i.Id == item.Id);
            _items[idx] = item;
        }
    }
}