using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVocabulary.ParserModule.Entities;
using OnlineVocabulary.ParserModule.Entities.LexemDefine;
using OnlineVocabulary.ParserModule.Entities.TreeDefine;
using OnlineVocabulary.ParserModule.Entities.Extensions;
using OnlineVocabulary.ParserModule.Entities.Helpers;

namespace OnlineVocabulary.ParserModule.SyntaxAnalyzer
{
    public class SyntaxAnalyzer
    {
        private readonly IUtilsHandler<Lexem, LexemType> UtilsHandler;
        private readonly IGrammarHandler<SyntaxTree, SyntaxNode> GrammarHandler;


        public List<SyntaxTree> GetSyntaxTrees
        {
            get 
            { 
                return GrammarHandler.GetSyntaxTrees.ToList(); 
            }
        }

        public SyntaxAnalyzer(Lexem[] Lexems) 
        {
            UtilsHandler = new UtilsHandler(Lexems);
            GrammarHandler = new GrammarHandler(UtilsHandler);
        }


   

        //
    }
}
