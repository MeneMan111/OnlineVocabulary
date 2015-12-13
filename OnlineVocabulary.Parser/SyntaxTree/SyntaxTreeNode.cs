using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OnlineVocabulary.Parser.LexicalAnalyzer;
using OnlineVocabulary.Parser.Entitys;

namespace OnlineVocabulary.Parser.SyntaxTree
{
    public class SyntaxTreeNode : LocationEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public List<Lexem> Lexems { get; set; }

        public ObjectId ParentId { get; set; }

        public SyntaxTreeNode()
        {
            Id = ObjectId.GenerateNewId();
            Lexems = new List<Lexem>();
        }

        public SyntaxTreeNode(ObjectId ParentId)
        {
            Id = ObjectId.GenerateNewId();
            Lexems = new List<Lexem>();
            this.ParentId = ParentId;
        }

        public override string ToString()
        {
            return String.Join(" ", Lexems.Select(ts => ts.Value));
        }
    }
}
