using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.ParserModule.Entities.TreeDefine
{
    public class TreeNodeList<T> : List<ITreeNode<T>>, ITreeNodeList<T>
    {
        public INode<T> Parent { get; set; }

        //
        public TreeNodeList(INode<T> parent)
        {
            Parent = parent;
        }

        //
        public new ITreeNode<T> Add(ITreeNode<T> node)
        {
            return Add(node, true);
        }

        public new bool Remove(ITreeNode<T> node)
        {
            return Remove(node, true);
        }

        public ITreeNode<T> Add(ITreeNode<T> node, bool updateParent)
        {
            if (updateParent)
            {
                node.SetParent(Parent, UpdateChildNodes: true);
                return node;
            }

            base.Add(node);
            return node;
        }

        public bool Remove(ITreeNode<T> node, bool updateParent)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (!Contains(node))
                return false;

            if (updateParent)
            {
                node.SetParent(null, UpdateChildNodes: false);
                return !Contains(node);
            }

            var result = base.Remove(node);
            return result;
        }

        public override string ToString()
        {
            return "Count=" + Count;
        }
    }
}
