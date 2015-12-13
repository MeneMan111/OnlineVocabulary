using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineVocabulary.Domain.Entities
{
    public class EntryTree : IEntity
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public List<Guid> Categories { get; set; }

        [BsonRequired]
        public List<EntryTreeNode> TreeNodes { get; set; }


        public EntryTree()
        {
            TreeNodes = new List<EntryTreeNode>();
        }
    }
}
