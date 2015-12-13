using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnlineVocabulary.Parser.LexicalAnalyzer
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
