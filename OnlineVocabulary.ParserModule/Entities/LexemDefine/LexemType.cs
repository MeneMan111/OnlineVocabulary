using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.ParserModule.Entities.LexemDefine
{
    public enum LexemType
    {
        EngKeyword,
        Transcription,
        EngAbbreviation,
        RusAbbreviation,
        NumHomonymValue,
        NumGrammFuncValue,
        NumLexValue,
        NumSubLexValue,
        TranslationReference,
        TranslationMeaning,

        //
        EOF
    }
}
