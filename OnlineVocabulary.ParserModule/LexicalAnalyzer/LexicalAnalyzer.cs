using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OnlineVocabulary.ParserModule.Entities;
using OnlineVocabulary.ParserModule.Entities.LexemDefine;
using OnlineVocabulary.ParserModule.Entities.TreeDefine;
using OnlineVocabulary.ParserModule.Entities.Extensions;
using OnlineVocabulary.ParserModule.Entities.Helpers;

namespace OnlineVocabulary.ParserModule.LexicalAnalyzer
{
    public class LexicalAnalyzer
    {
        //private RegexLexemDefinition[] RegexLexemDefinitions;

        private readonly RegexLexemDefinition[] RegexLexemDefinitions =
		{
            new RegexLexemDefinition(@"([A-z]|[-]|[\s])*(?=\s{2})", LexemType.EngKeyword),
            new RegexLexemDefinition(@"\s*\[(.*?)\]", LexemType.Transcription),
            new RegexLexemDefinition(@"\s*_([A-z]|[-])*\.", LexemType.EngAbbreviation),
            new RegexLexemDefinition(@"\s*_[А-я]*\.", LexemType.RusAbbreviation),
            new RegexLexemDefinition(@"\s*_[IVX]*", LexemType.NumHomonymValue),
            new RegexLexemDefinition(@"\s*\d\.", LexemType.NumGrammFuncValue),
            new RegexLexemDefinition(@"\s*\d>", LexemType.NumLexValue),
            new RegexLexemDefinition(@"\s*=(.*?)((?=\s*_(.*?)\.)|(?=\s*([А-я])>)|(?=\s*\d\.)|(?=\s*\d>)|(?=\s*_[IVX]*)|(?=([A-z]|[-]|[\s])*\s{2})|\z)", LexemType.TranslationReference),
            new RegexLexemDefinition(@"(.*?)((?=\s*\[(.*?)\])|(?=\s*_(.*?)\.)|(?=\s*([А-я])>)|(?=\s*\d\.)|(?=\s*\d>)|(?=\s*_[IVX]*)|(?=([A-z]|[-]|[\s])*\s{2})|\z)", LexemType.TranslationMeaning)
		};

        private TextReader _TextReader;

        private LexicalAnalyzerEngine _LexicalAnalyzerEngine;

        public List<Lexem> Lexems { get; private set; }


        //
        public LexicalAnalyzer(string FileString)
        {
            _TextReader = new StringReader(FileString);
            _LexicalAnalyzerEngine = new LexicalAnalyzerEngine(_TextReader, RegexLexemDefinitions);
            searchMatchedLexem();
            Lexems.Add(new Lexem(LexemType.EOF, ""));
        }

        public LexicalAnalyzer(string FileString, List<KeyValuePair<string, LexemType>> _RegexLexemDefinitions)
        {
            _TextReader = new StringReader(FileString);
            RegexLexemDefinitions = _RegexLexemDefinitions.Select(lexem => new RegexLexemDefinition(lexem.Key, lexem.Value)).ToArray();
            _LexicalAnalyzerEngine = new LexicalAnalyzerEngine(_TextReader, RegexLexemDefinitions);
            searchMatchedLexem();
            Lexems.Add(new Lexem(LexemType.EOF, ""));
        }

        private void searchMatchedLexem()
        {
            Lexems = new List<Lexem>();
            while (_LexicalAnalyzerEngine.Next())
            {
                Lexems.Add(_LexicalAnalyzerEngine.CurrentMatchLexem);
            }
        }

        //
    }
}
