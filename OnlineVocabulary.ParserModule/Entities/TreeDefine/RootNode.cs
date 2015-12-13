using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.ParserModule.Entities.TreeDefine
{
    public class RootNode<T> : Entity, INode<T>
    {
        public T Value { get; set; }

        public ITreeNodeList<T> Children { get; set; }

        public RootNode()
        {
            Children = new TreeNodeList<T>(this);
        }

        public RootNode(T Value)
        {
            this.Value = Value;
            Children = new TreeNodeList<T>(this);
        }
    }
}
