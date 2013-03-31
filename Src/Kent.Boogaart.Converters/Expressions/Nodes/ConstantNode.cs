namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    // a node to hold a constant value
    internal sealed class ConstantNode<T> : Node
    {
        private readonly T value;

        public ConstantNode(T value)
        {
            this.value = value;
        }

        public T Value
        {
            get { return this.value; }
        }

        public override object Evaluate(NodeEvaluationContext evaluationContext)
        {
            return this.value;
        }
    }
}
