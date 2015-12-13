using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.Parser.LexicalAnalyzer
{
    public enum LexemType
    {
        EngKeyword,
        Transcription,
        EngAbbreviation,
        RusAbbreviation,
        NumMomonyumValue,
        NumGrammFuncValue,
        NumLexValue,
        NumSubLexValue,
        TranslationReference,
        TranslationMeaning,

        //
        EOF
    }
}
