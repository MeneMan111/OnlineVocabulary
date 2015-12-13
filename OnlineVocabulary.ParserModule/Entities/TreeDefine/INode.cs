using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.ParserModule.Entities.TreeDefine
{
    public interface INode<T>
    {
        T Value { get; set; }

        ITreeNodeList<T> Children { get; }
    }
}
