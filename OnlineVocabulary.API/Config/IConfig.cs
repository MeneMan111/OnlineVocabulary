using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineVocabulary.API.Config
{
    public interface IConfig
    {
        IQueryable<MongoDbConnection> mongoDbConnections { get; }
    }
}