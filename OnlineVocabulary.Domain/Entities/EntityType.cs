using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OnlineVocabulary.Domain.Entities
{
    public class EntityType
    {
        [BsonId]
        public Guid Id;

        [BsonRequired]
        public string Name;

        [BsonRequired]
        public string Type;


        public override string ToString()
        {
            var format = string.IsNullOrEmpty(Type) ? "{0}" : "{0}({1})";
            return string.Format(format, Type, Type);
        }

    }
}
