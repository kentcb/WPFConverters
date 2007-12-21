using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node to negate a boolean value
	internal sealed class NotNode : UnaryNode
	{
		public NotNode(Node node)
			: base(node)
		{
		}

		public override object Evaluate(NodeEvaluationContext evaluationContext)
		{
			object value = Node.Evaluate(evaluationContext);
			NodeValueType nodeValueType = Node.GetNodeValueType(value);
			ExceptionHelper.ThrowIf(nodeValueType != NodeValueType.Boolean, "NotBooleanType", nodeValueType);
			return !((bool) value);
		}
	}
}
