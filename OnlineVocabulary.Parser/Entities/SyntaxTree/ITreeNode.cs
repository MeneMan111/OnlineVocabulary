using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.Parser.Entities
{
    public interface ITreeNode
    {
        IEnumerable<ITreeNode> Ancestors { get; }

        ITreeNode ParentNode { get; }

        IEnumerable<ITreeNode> ChildNodes { get; }

        IEnumerable<ITreeNode> Descendants { get; }

        void OnAncestorChanged(NodeChangeType changeType, ITreeNode node);

        void OnDescendantChanged(NodeChangeType changeType, ITreeNode node);

        int Depth { get; }

        void OnDepthChanged();

        int Height { get; }

        void OnHeightChanged();
    }
}
