using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.ParserModule.Entities.TreeDefine
{
    public class Tree<T> : Entity
    {
        public RootNode<T> RootNode { get; private set; }

        public Tree()
        {
            RootNode = new RootNode<T>();
        }

        public Tree(T Value)
        {
            RootNode = new RootNode<T>(Value);
        }

        public IEnumerable<INode<T>> GetTreeNodesByDFS()
        {
            Stack<INode<T>> NotVisitedNodesStack = new Stack<INode<T>>();

            INode<T> InstanceNode = RootNode;
            NotVisitedNodesStack.Push(InstanceNode);
            while (NotVisitedNodesStack.Count != 0)
            {
                InstanceNode = NotVisitedNodesStack.Pop();
                if (!InstanceNode.Children.Count().Equals(0))
                {
                    foreach (var tnode in InstanceNode.Children.AsEnumerable().Reverse())
                    {
                        NotVisitedNodesStack.Push(tnode);
                    }
                }
                yield return InstanceNode;
            }
        }

    }
}
