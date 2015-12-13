using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVocabulary.ParserModule.Entities.TreeDefine;

namespace OnlineVocabulary.Domain.Services
{
    public interface IParserService
    {
        OperationResult<List<SyntaxTree>> TryParseString(string parseString);
    }
}
