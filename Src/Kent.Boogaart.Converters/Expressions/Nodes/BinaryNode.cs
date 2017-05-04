namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    using System.Diagnostics;

    // a node containing two children
    internal abstract class BinaryNode : Node
    {
        private readonly Node leftNode;
        private readonly Node rightNode;

        protected BinaryNode(Node leftNode, Node rightNode)
        {
            Debug.Assert(leftNode != null);
            Debug.Assert(rightNode != null);
            this.leftNode = leftNode;
            this.rightNode = rightNode;
        }

        public Node LeftNode
        {
            get { return this.leftNode; }
        }

        public Node RightNode
        {
            get { return this.rightNode; }
        }

        protected abstract string OperatorSymbols
        {
            get;
        }
    }
}
