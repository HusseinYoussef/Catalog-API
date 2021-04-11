using System;
using System.Collections.Generic;
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
        public void CreateItem(Item item)
        {
            itemCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            itemCollection.DeleteOne(i => i.Id == id);
        }

        public Item GetItem(Guid id)
        {
            return itemCollection.Find(i => i.Id == id).FirstOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemCollection.Find(i => true).ToList();
        }

        public void UpdateItem(Item item)
        {
            itemCollection.ReplaceOne(i => i.Id == item.Id, item);
        }
    }
}