using System.Diagnostics;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    //a node to complement an integral value
    internal sealed class ComplementNode : UnaryNode
    {
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(ComplementNode));

        public ComplementNode(Node node)
            : base(node)
        {
        }

        public override object Evaluate(NodeEvaluationContext evaluationContext)
        {
            object value = Node.Evaluate(evaluationContext);
            NodeValueType nodeValueType = GetNodeValueType(value);
            exceptionHelper.ResolveAndThrowIf(!IsIntegralNodeValueType(nodeValueType), "NotIntegralType", nodeValueType);

            switch (nodeValueType)
            {
                case NodeValueType.Byte:
                    return ~((byte) value);
                case NodeValueType.Int16:
                    return ~((short) value);
                case NodeValueType.Int32:
                    return ~((int) value);
                case NodeValueType.Int64:
                    return ~((long) value);
            }

            Debug.Assert(false);
            return null;
        }
    }
}
