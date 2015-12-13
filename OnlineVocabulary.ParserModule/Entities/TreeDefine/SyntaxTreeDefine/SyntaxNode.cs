using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVocabulary.ParserModule.Entities.LexemDefine;

namespace OnlineVocabulary.ParserModule.Entities.TreeDefine
{
    public class SyntaxNode : TreeNode<List<Lexem>>
    {
        public SyntaxNode(params Lexem[] lexems) 
            : base(lexems.ToList()) { }

        public SyntaxNode(SyntaxNode node, params Lexem[] lexems)
            : base(lexems.ToList(), node) { }
    }
}
