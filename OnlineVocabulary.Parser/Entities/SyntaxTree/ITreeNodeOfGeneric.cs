using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.Parser.Entities
{
    public interface ITreeNode<T> : ITreeNode
    {
        T Value { get; set; }

        ITreeNode<T> Root { get; }

        ITreeNode<T> Parent { get; set; }

        TreeNodeList<T> Children { get; }

        void SetParent(ITreeNode<T> Node, bool UpdateChildNodes = true);

    }
}
