using System.Diagnostics;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    // a node containing three children
    internal abstract class TernaryNode : Node
    {
        private readonly Node _firstNode;
        private readonly Node _secondNode;
        private readonly Node _thirdNode;

        public Node FirstNode
        {
            get
            {
                return _firstNode;
            }
        }

        public Node SecondNode
        {
            get
            {
                return _secondNode;
            }
        }

        public Node ThirdNode
        {
            get
            {
                return _thirdNode;
            }
        }

        protected abstract string OperatorSymbols
        {
            get;
        }

        protected TernaryNode(Node firstNode, Node secondNode, Node thirdNode)
        {
            Debug.Assert(firstNode != null);
            Debug.Assert(secondNode != null);
            Debug.Assert(thirdNode != null);
            _firstNode = firstNode;
            _secondNode = secondNode;
            _thirdNode = thirdNode;
        }
    }
}
