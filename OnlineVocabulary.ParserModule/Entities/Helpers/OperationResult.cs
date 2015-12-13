using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.ParserModule.Entities.Helpers
{
    public class OperationResult
    {
        public bool IsSuccess { get; private set; }

        public OperationResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        //
    }
}
