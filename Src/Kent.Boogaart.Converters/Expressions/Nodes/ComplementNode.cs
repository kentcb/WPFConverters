using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node to complement an integral value
	internal sealed class ComplementNode : UnaryNode
	{
		public ComplementNode(Node node)
			: base(node)
		{
		}

		public override object Evaluate(NodeEvaluationContext evaluationContext)
		{
			object value = Node.Evaluate(evaluationContext);
			NodeValueType nodeValueType = Node.GetNodeValueType(value);
			ExceptionHelper.ThrowIf(!Node.IsIntegralNodeValueType(nodeValueType), "NotIntegralType", nodeValueType);

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
