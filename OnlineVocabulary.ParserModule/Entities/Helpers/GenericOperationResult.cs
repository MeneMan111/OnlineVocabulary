using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.ParserModule.Entities.Helpers
{
    public class OperationResult<IEntity> : OperationResult
    {
        public OperationResult(bool isSuccess)
            : base(isSuccess) { }

        public IEntity Entity { get; set; }
    }
}
