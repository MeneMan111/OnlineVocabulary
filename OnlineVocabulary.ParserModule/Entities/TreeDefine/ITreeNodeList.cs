using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.ParserModule.Entities.TreeDefine
{
    public interface ITreeNodeList<T> : IList<ITreeNode<T>>
    {
        new ITreeNode<T> Add(ITreeNode<T> node);

        new bool Remove(ITreeNode<T> node);

        ITreeNode<T> Add(ITreeNode<T> node, bool updateParent);

        bool Remove(ITreeNode<T> node, bool updateParent);
    }
}
