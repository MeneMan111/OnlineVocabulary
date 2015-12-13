using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OnlineVocabulary.Parser.LexicalAnalyzer;

namespace OnlineVocabulary.Parser.SyntaxTree
{
    public class SyntaxTree
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public List<SyntaxTreeNode> TreeNodes { get; set; }

        public SyntaxTree()
        {
            Id = ObjectId.GenerateNewId();
            TreeNodes = new List<SyntaxTreeNode>();
        }
    }
}
