using System;
using System.Collections.Generic;
using Catalog_API.Models;

namespace Catalog_API.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetItems();
        Item GetItem(Guid id);
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Guid id);
    }
}