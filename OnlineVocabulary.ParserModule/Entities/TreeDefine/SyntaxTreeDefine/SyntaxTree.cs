using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVocabulary.ParserModule.Entities.LexemDefine;

namespace OnlineVocabulary.ParserModule.Entities.TreeDefine
{
    public class SyntaxTree : Tree<List<Lexem>>
    {
        public SyntaxTree(params Lexem[] lexems) 
            : base(lexems.ToList()) { }

        public override string ToString()
        {
            var trees = GetTreeNodesByDFS();
            var TreeAsString = trees.Select(t => string.Join("|", t.Value.Select(l => l.Value)));
            return string.Join("|||", TreeAsString);
        }
    }
}
