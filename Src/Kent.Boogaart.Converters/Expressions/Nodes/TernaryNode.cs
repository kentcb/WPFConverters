namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    using System.Diagnostics;

    // a node containing three children
    internal abstract class TernaryNode : Node
    {
        private readonly Node firstNode;
        private readonly Node secondNode;
        private readonly Node thirdNode;

        protected TernaryNode(Node firstNode, Node secondNode, Node thirdNode)
        {
            Debug.Assert(firstNode != null);
            Debug.Assert(secondNode != null);
            Debug.Assert(thirdNode != null);

            this.firstNode = firstNode;
            this.secondNode = secondNode;
            this.thirdNode = thirdNode;
        }

        public Node FirstNode
        {
            get { return this.firstNode; }
        }

        public Node SecondNode
        {
            get { return this.secondNode; }
        }

        public Node ThirdNode
        {
            get { return this.thirdNode; }
        }

        protected abstract string OperatorSymbols
        {
            get;
        }
    }
}
