using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using OnlineVocabulary.ParserModule.Entities;
using OnlineVocabulary.ParserModule.Entities.LexemDefine;
using OnlineVocabulary.ParserModule.Entities.TreeDefine;
using OnlineVocabulary.ParserModule.Entities.Extensions;
using OnlineVocabulary.ParserModule.Entities.Helpers;

namespace OnlineVocabulary.ParserModule.LexicalAnalyzer
{
    public class LexicalAnalyzerEngine : IDisposable
    {
        private TextReader TReader;

        private RegexLexemDefinition[] TokenDefinitions;

        private string LineRemaining;

        private int LineNumber;

        private int Position;

        public Lexem CurrentMatchLexem { get; private set; }


        //
        public LexicalAnalyzerEngine(TextReader reader, RegexLexemDefinition[] tokenDefinitions)
        {
            TReader = reader;
            TokenDefinitions = tokenDefinitions;
            nextLine();
        }


        private void nextLine()
        {
            do
            {
                LineRemaining = TReader.ReadLine();
                ++LineNumber;
                Position = 0;
            } while (LineRemaining != null && LineRemaining.Length == 0);
        }

        public bool Next()
        {
            if (LineRemaining == null)
            {
                return false;
            }

            foreach (var tokendef in TokenDefinitions)
            {
                var matched = tokendef.Matcher.Match(LineRemaining);
                if (matched > 0)
                {
                    Position += matched;
                    CurrentMatchLexem = new Lexem(tokendef.Type, LineRemaining.Substring(0, matched));
                    LineRemaining = LineRemaining.Substring(matched);

                    if (LineRemaining.Length == 0)
                    {
                        nextLine();
                    }
                    return true;
                }
            }
            throw new Exception(string.Format("Unable to match against any tokens at line {0} position {1} \"{2}\"", LineNumber, Position, LineRemaining));
        }

        public void Dispose()
        {
            TReader.Dispose();
        }
    }
}
