using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVocabulary.ParserModule.Entities;
using OnlineVocabulary.ParserModule.Entities.LexemDefine;
using OnlineVocabulary.ParserModule.Entities.TreeDefine;
using OnlineVocabulary.ParserModule.Entities.Extensions;
using OnlineVocabulary.ParserModule.Entities.Helpers;

namespace OnlineVocabulary.ParserModule.SyntaxAnalyzer
{
    public class UtilsHandler : IUtilsHandler<Lexem,LexemType>
    {
        private readonly Lexem[] Lexems;

        private int LexemId;


        public UtilsHandler(Lexem[] Lexems) 
        {
            this.Lexems = Lexems;
            LexemId = 0;
        }



        #region Lexem handlers

        public void Skip(int count = 1)
        {
            LexemId = Math.Min(LexemId + count, Lexems.Length - 1);
        }

        public bool Peek(params LexemType[] types)
        {
            var id = Math.Min(LexemId, Lexems.Length - 1);
            var lex = Lexems[id];
            return lex.Type.IsAnyOf(types);
        }

        public bool Check(LexemType lexem)
        {
            var lex = Lexems[LexemId];
            if (lex.Type != lexem) 
            {
                return false;
            }

            Skip();
            return true;
        }

        public OperationResult<Lexem> Attempt(LexemType type)
        {
            var lex = Lexems[LexemId];

            if (lex.Type != type)
            {
                return new OperationResult<Lexem>(false);
            }
            else
            {
                Skip();
                return new OperationResult<Lexem>(true)
                {
                    Entity = lex
                };
            }
        }

        public Lexem Ensure(LexemType type)
        {
            var lex = Lexems[LexemId];

            if (lex.Type != type)
            {
                throw new Exception(string.Format("Lexem Type  {0} don't equals {1} ", lex.Type, type));
            }
            Skip();
            return lex;
        }

        #endregion



        #region Node handlers

        public OperationResult<T> Attempt<T>(Func<T> getter) where T : Entity
        {
            var backup = LexemId;
            var result = Bind(getter);
            if (result == null)
            {
                LexemId = backup;
                return new OperationResult<T>(false);
            }
            else
            {
                return new OperationResult<T>(true)
                {
                    Entity = result
                };
            }
        }

        public OperationResult<List<T>> Attempt<T>(Func<List<T>> getter) where T : Entity
        {
            var backup = LexemId;
            var result = getter();
            if (result == null || result.Count == 0) 
            {
                LexemId = backup;
                return new OperationResult<List<T>>(false);
            }
            else
            {
                return new OperationResult<List<T>>(true)
                {
                    Entity = result
                };
            }
        }

        public T Ensure<T>(Func<T> getter, string msg) where T : Entity
        {
            var result = Bind(getter);
            if (result == null)
                throw new Exception(msg);

            return result;
        }

        public T Bind<T>(Func<T> getter) where T : Entity
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
