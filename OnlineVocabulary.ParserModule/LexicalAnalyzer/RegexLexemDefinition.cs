using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OnlineVocabulary.ParserModule.Entities;
using OnlineVocabulary.ParserModule.Entities.LexemDefine;
using OnlineVocabulary.ParserModule.Entities.TreeDefine;
using OnlineVocabulary.ParserModule.Entities.Extensions;
using OnlineVocabulary.ParserModule.Entities.Helpers;

namespace OnlineVocabulary.ParserModule.LexicalAnalyzer
{
    public class RegexLexemDefinition
    {
        public IMatcher Matcher;

        public LexemType Type;

        public RegexLexemDefinition(string RegexPatter, LexemType Type)
        {
            Matcher = new RegexMatcher(RegexPatter);
            this.Type = Type;
        }

    }
}
