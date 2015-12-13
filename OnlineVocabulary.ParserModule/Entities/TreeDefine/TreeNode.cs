using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.ParserModule.Entities.TreeDefine
{
    public class TreeNode<T> : Entity, ITreeNode<T>
    {
        protected T _Value;

        public T Value
        {
            get { return _Value; }
            set
            {
                if (value == null && _Value == null)
                    return;

                if (value != null && _Value != null && value.Equals(_Value))
                    return;

                _Value = value;
            }
        }

        protected INode<T> _Parent;

        public INode<T> Parent
        {
            get { return _Parent; }
            set { SetParent(value, true); }
        }

        protected ITreeNodeList<T> _ChildNodes;

        public ITreeNodeList<T> Children
        {
            get { return _ChildNodes; }
        }


        //
        public TreeNode()
        {
            _Parent = null;
            _ChildNodes = new TreeNodeList<T>(this);
        }

        public TreeNode(T Value)
        {
            this.Value = Value;
            _Parent = null;
            _ChildNodes = new TreeNodeList<T>(this);
        }

        public TreeNode(T Value, TreeNode<T> Parent)
        {
            this.Value = Value;
            _Parent = Parent;
            _ChildNodes = new TreeNodeList<T>(this);
        }


        //
        public void SetParent(INode<T> node, bool updateChildNodes = true)
        {
            if (node == Parent)
                return;

            var oldParent = Parent;

            if (oldParent != null && oldParent.Children.Contains(this))
                oldParent.Children.Remove(this, updateParent: false);

            _Parent = node;

            if (_Parent != null && updateChildNodes)
                _Parent.Children.Add(this, updateParent: false);
        }
    }
}
