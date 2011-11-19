using System.Diagnostics;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    internal sealed class NodeEvaluationContext
    {
        private readonly object[] arguments;

        public static readonly NodeEvaluationContext Empty = new NodeEvaluationContext(new object[] { });

        public NodeEvaluationContext(object[] arguments)
        {
            Debug.Assert(arguments != null);
            this.arguments = arguments;
        }

        public bool HasArgument(int index)
        {
            Debug.Assert(index >= 0);
            return index < this.arguments.Length;
        }

        public object GetArgument(int index)
        {
            Debug.Assert(index >= 0);
            return this.arguments[index];
        }
    }
}
