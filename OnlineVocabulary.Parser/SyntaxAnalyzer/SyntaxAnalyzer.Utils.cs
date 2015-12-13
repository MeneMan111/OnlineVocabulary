using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using OnlineVocabulary.Parser.LexicalAnalyzer;
using OnlineVocabulary.Parser.Extensions;
using OnlineVocabulary.Parser.Entitys;

namespace OnlineVocabulary.Parser.SyntaxAnalyzer
{
    partial class SyntaxAnalyzer
    {

        #region Lexem handlers

        [DebuggerStepThrough]
        private bool Peek(params LexemType[] types)
        {
            var id = Math.Min(LexemId, Lexems.Length - 1);
            var lex = Lexems[id];
            return lex.Type.IsAnyOf(types);
        }

        [DebuggerStepThrough]
        private Lexem Ensure(LexemType type, string msg, params object[] args)
        {
            var lex = Lexems[LexemId];

            if (lex.Type != type)
            {
                throw new Exception(string.Format("Lexem Type  {0} don't equals {1} ", lex.Type, type));
            }
            Skip();
            return lex;
        }

        [DebuggerStepThrough]
        private string getValue()
        {
            var value = Lexems[LexemId].Value;
            Skip();
            return value;
        }


        [DebuggerStepThrough]
        private Lexem getCurrentLexem()
        {
            var value = Lexems[LexemId];
            Skip();
            return value;
        }


        [DebuggerStepThrough]
        private bool Check(LexemType lexem)
        {
            var lex = Lexems[LexemId];

            if (lex.Type != lexem)
                return false;

            Skip();
            return true;
        }

        [DebuggerStepThrough]
        private void Skip(int count = 1)
        {
            LexemId = Math.Min(LexemId + count, Lexems.Length - 1);
        }

        #endregion


        #region Node handlers

        [DebuggerStepThrough]
        private T Attempt<T>(Func<T> getter) where T : LocationEntity
        {
            var backup = LexemId;
            var result = Bind(getter);
            if (result == null)
                LexemId = backup;

            return result;
        }

        [DebuggerStepThrough]
        private List<T> Attempt<T>(Func<List<T>> getter)
        {
            var backup = LexemId;
            var result = getter();
            if (result == null || result.Count == 0)
                LexemId = backup;
            return result;
        }

        [DebuggerStepThrough]
        private T Ensure<T>(Func<T> getter, string msg) where T : LocationEntity
        {
            var result = Bind(getter);
            if (result == null)
                throw new Exception(msg);

            return result;
        }

        [DebuggerStepThrough]
        private T Bind<T>(Func<T> getter) where T : LocationEntity
        {
            var startId = LexemId;
            var start = Lexems[LexemId];

            var result = getter();

            if (result != null)
            {
                result.StartLocation = start.StartLocation;

                var endId = LexemId;
                if (endId > startId && endId > 0)
                    result.EndLocation = Lexems[LexemId - 1].EndLocation;
            }

            return result;
        }

        #endregion
    }
}
