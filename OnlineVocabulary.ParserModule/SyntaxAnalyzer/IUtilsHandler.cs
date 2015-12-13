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
    public interface IUtilsHandler<T, TType> where T : Entity
    {
        #region Entities Utils Handlers

        void Skip(int count);

        bool Peek(params TType[] types);

        bool Check(TType type);

        OperationResult<T> Attempt(TType type);

        T Ensure(TType type);

        #endregion


        #region Grammar Utils Handlers

        OperationResult<G> Attempt<G>(Func<G> getter) where G : Entity;

        OperationResult<List<G>> Attempt<G>(Func<List<G>> getter) where G : Entity;

        G Ensure<G>(Func<G> getter, string msg) where G : Entity;

        G Bind<G>(Func<G> getter) where G : Entity;

        #endregion
    }
}
