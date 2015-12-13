using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace OnlineVocabulary.API.Config
{
    public class MongoDbConnectionConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("mongoDbConnections")]
        public MongoDbConnectionCollection mongoDbConnections
        {
            get
            {
                return this["mongoDbConnections"] as MongoDbConnectionCollection;
            }
        }
    }

    public class MongoDbConnectionCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new MongoDbConnection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MongoDbConnection)element).Name;
        }
    }

    public class MongoDbConnection : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
        }

        [ConfigurationProperty("connection", IsRequired = true)]
        public string Connection
        {
            get
            {
                return this["connection"] as string;
            }
        }

        [ConfigurationProperty("database", IsRequired = true)]
        public string Database
        {
            get
            {
                return this["database"] as string;
            }
        }

        //

        [ConfigurationProperty("user", IsRequired = false)]
        public string User
        {
            get
            {
                return this["user"] as string;
            }
        }

        [ConfigurationProperty("password", IsRequired = false)]
        public string Password
        {
            get
            {
                return this["password"] as string;
            }
        }
    }
}