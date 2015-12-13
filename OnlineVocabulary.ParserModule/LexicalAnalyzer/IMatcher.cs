using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.ParserModule.LexicalAnalyzer
{
    public interface IMatcher
    {
        int Match(string SourceText);
    }
}
