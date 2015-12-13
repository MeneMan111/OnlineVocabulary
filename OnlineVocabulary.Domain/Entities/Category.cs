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
    public class Category : IEntity
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonRequired]
        public string Abbreviation { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public string Type { get; set; }


        public List<string> Names { get; set; }


        public Category()
        {
            Names = new List<string>();
        }
    }
}
