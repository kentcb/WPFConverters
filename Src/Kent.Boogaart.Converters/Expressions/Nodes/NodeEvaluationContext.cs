namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    using System.Diagnostics;

    internal sealed class NodeEvaluationContext
    {
        private readonly object[] arguments;

        public static readonly NodeEvaluationContext Empty = new NodeEvaluationContext(new object[] { });

        private NodeEvaluationContext(object[] arguments)
        {
            Debug.Assert(arguments != null);
            this.arguments = arguments;
        }

        public static NodeEvaluationContext Create(params object[] arguments)
        {
            if (arguments == null || arguments.Length == 0)
            {
                return Empty;
            }

            return new NodeEvaluationContext(arguments);
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
