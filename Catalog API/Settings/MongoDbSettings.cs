using System;

namespace Catalog_API.Settings
{
    public class MongoDbSettings
    { 
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string ConnectionString
        {
            get
            {
                return $"mongodb://{Host}:{Port}";
            }
        }
    }
}