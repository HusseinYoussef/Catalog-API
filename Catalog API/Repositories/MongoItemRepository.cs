using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog_API.Models;
using Catalog_API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog_API.Repositories
{
    public class MongoItemRepository : IItemRepository
    {
        private readonly IMongoCollection<Item> itemCollection;

        public MongoItemRepository(IOptions<MongoDbSettings> settings)
        {
            // Mongo Driver will create the Database and Collection when needed
            IMongoClient mongoClint = new MongoClient(settings.Value.ConnectionString);
            IMongoDatabase mongoDatabase = mongoClint.GetDatabase(settings.Value.DatabaseName);
            itemCollection = mongoDatabase.GetCollection<Item>(settings.Value.CollectionName);
        }
        public async Task CreateItemAsync(Item item)
        {
            await itemCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            await itemCollection.DeleteOneAsync(i => i.Id == id);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            return await itemCollection.Find(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(string query)
        {
            if (!String.IsNullOrEmpty(query))
            { 
                return await itemCollection.Find(i => i.Name.ToLower().Contains(query.ToLower())).ToListAsync();
            }
            return await itemCollection.Find(i => true).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            await itemCollection.ReplaceOneAsync(i => i.Id == item.Id, item);
        }
    }
}