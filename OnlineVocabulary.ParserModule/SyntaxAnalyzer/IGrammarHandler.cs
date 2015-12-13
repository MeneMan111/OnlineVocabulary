using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVocabulary.ParserModule.Entities.TreeDefine;

namespace OnlineVocabulary.ParserModule.SyntaxAnalyzer
{
    public interface IGrammarHandler<T,N>
    {
        List<T> GetSyntaxTrees { get; }

        IEnumerable<T> parseMain();

        T parseVocabularyEntry();

        N parseHomonymValue();

        N parseGrammarFuncValue();

        N parseLexicalValue();

        N parseTranslationValue();

    }
}
