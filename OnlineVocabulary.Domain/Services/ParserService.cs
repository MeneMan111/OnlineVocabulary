using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVocabulary.ParserModule.Entities.TreeDefine;
using OnlineVocabulary.ParserModule.LexicalAnalyzer;
using OnlineVocabulary.ParserModule.SyntaxAnalyzer;

namespace OnlineVocabulary.Domain.Services
{
    public class ParserService : IParserService
    {
        private LexicalAnalyzer LexAnalyzer;

        private SyntaxAnalyzer SynAnalyzer;

        public OperationResult<List<SyntaxTree>> TryParseString(string parseString) 
        {
            LexAnalyzer = new LexicalAnalyzer(parseString);
            if (LexAnalyzer.Lexems.Count().Equals(0)) 
            {
                return new OperationResult<List<SyntaxTree>>(false);
            }

            SynAnalyzer = new SyntaxAnalyzer(LexAnalyzer.Lexems.ToArray());

            if (!SynAnalyzer.GetSyntaxTrees.Count().Equals(0))
            {
                return new OperationResult<List<SyntaxTree>>(true)
                {
                    Entity = SynAnalyzer.GetSyntaxTrees
                };
            }
            else
            {
                return new OperationResult<List<SyntaxTree>>(false);
            }

            //
        }
    }
}
