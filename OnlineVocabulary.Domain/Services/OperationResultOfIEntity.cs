using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVocabulary.Domain.Entities;

namespace OnlineVocabulary.Domain.Services
{
    public class OperationResult<IEntity> : OperationResult 
    {
        public OperationResult(bool isSuccess) 
            : base(isSuccess) { }

        public IEntity Entity { get; set; }
    }
}
