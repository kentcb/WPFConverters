using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node to negate a numerical value
	internal sealed class NegateNode : UnaryNode
	{
		public NegateNode(Node node)
			: base(node)
		{
		}

		public override object Evaluate(NodeEvaluationContext evaluationContext)
		{
			object value = Node.Evaluate(evaluationContext);
			NodeValueType nodeValueType = GetNodeValueType(value);

			switch (nodeValueType)
			{
				case NodeValueType.Byte:
					return -1 * (byte) value;
				case NodeValueType.Int16:
					return -1 * (short) value;
				case NodeValueType.Int32:
					return -1 * (int) value;
				case NodeValueType.Int64:
					return -1 * (long) value;
				case NodeValueType.Single:
					return -1 * (float) value;
				case NodeValueType.Double:
					return -1 * (double) value;
				case NodeValueType.Decimal:
					return -1 * (decimal) value;
			}

			ExceptionHelper.Throw("CannotNegateValue", nodeValueType);
			return null;
		}
	}
}
