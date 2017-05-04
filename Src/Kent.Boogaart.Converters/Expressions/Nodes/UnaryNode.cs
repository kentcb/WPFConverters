namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    using System.Diagnostics;

    // a node with a single child node
    internal abstract class UnaryNode : Node
    {
        private readonly Node node;

        protected UnaryNode(Node node)
        {
            Debug.Assert(node != null);
            this.node = node;
        }

        public Node Node
        {
            get { return this.node; }
        }
    }
}
