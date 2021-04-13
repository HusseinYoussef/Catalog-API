using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog_API.Models;

namespace Catalog_API.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItemsAsync(string query=null);
        Task<Item> GetItemAsync(Guid id);
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
    }
}