using System;

namespace Catalog_API.Settings
{
    public class MysqlDbSettings
    { 
        public string Host { get; set; }
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string ConnectionString 
        {
            get
            {
                return $"Server={Host};Port=3306;Database={DatabaseName};Uid={Username};Pwd={Password}";
            }
        }

    }
}