using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnlineVocabulary.ParserModule.LexicalAnalyzer
{
    public class RegexMatcher : IMatcher
    {
        private Regex _Regex;

        public int Match(string SourceText)
        {
            var match = _Regex.Match(SourceText);
            return match.Success ? match.Length : 0;
        }

        //
        public override string ToString()
        {
            return _Regex.ToString();
        }
        //

        public RegexMatcher(string RegexPatter)
        {
            _Regex = new Regex(string.Format("^{0}", RegexPatter));
        }
    }
}
