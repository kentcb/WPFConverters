using System.Diagnostics;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    // a node to hold a constant value
    internal sealed class ConstantNode<T> : Node
    {
        private readonly T value;

        public T Value
        {
            get { return this.value; }
        }

        public ConstantNode(T value)
        {
            Debug.Assert(value != null);
            this.value = value;
        }

        public override object Evaluate(NodeEvaluationContext evaluationContext)
        {
            return this.value;
        }
    }
}
