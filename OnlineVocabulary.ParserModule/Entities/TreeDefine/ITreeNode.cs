using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.ParserModule.Entities.TreeDefine
{
    public interface ITreeNode<T> : INode<T>
    {
        INode<T> Parent { get; set; }

        void SetParent(INode<T> Node, bool UpdateChildNodes = true);
    }
}
