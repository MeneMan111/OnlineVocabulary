using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace OnlineVocabulary.API.Config
{
    public class APIConfig : IConfig
    {
        public IQueryable<MongoDbConnection> mongoDbConnections
        {
            get
            {
                MongoDbConnectionConfigSection _mongoDbConnections = (MongoDbConnectionConfigSection)ConfigurationManager.GetSection("mongoDbConnectionConfig");
                return _mongoDbConnections.mongoDbConnections.OfType<MongoDbConnection>().AsQueryable<MongoDbConnection>();
            }
        }
    }
}