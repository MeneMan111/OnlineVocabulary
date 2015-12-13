using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineVocabulary.Domain.Entities
{
    public class EntryTreeNode : IEntity
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonRequired]
        public List<Lexem> Lexems { get; set; }

        [BsonRequired]
        public Guid ParentId { get; set; }


        public EntryTreeNode()
        {
            Lexems = new List<Lexem>();
        }

        public bool ExistType(string _LexemType)
        {
            return Lexems.Exists(lexem => lexem.Type == _LexemType);
        }

        public override string ToString()
        {
            return String.Join(" ", Lexems.Select(ts => ts.Value));
        }
    }
}
