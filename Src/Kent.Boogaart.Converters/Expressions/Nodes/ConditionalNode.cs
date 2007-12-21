using System;
using System.Collections.Generic;
using System.Text;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node from which conditional nodes will inherit
	internal abstract class ConditionalNode : BinaryNode
	{
		protected ConditionalNode(Node leftNode, Node rightNode)
			: base(leftNode, rightNode)
		{
		}

		public override object Evaluate(NodeEvaluationContext evaluationContext)
		{
			object leftNodeValue = LeftNode.Evaluate(evaluationContext);
			NodeValueType leftNodeValueType = Node.GetNodeValueType(leftNodeValue);

			if (leftNodeValueType == NodeValueType.Boolean)
			{
				//give base a chance to yield a result without evaluating the right node
				bool? result = DetermineResultPreRightEvaluation((bool) leftNodeValue);

				if (result.HasValue)
				{
					return result.Value;
				}
			}

			object rightNodeValue = RightNode.Evaluate(evaluationContext);
			NodeValueType rightNodeValueType = Node.GetNodeValueType(rightNodeValue);
			ExceptionHelper.ThrowIf(leftNodeValueType != NodeValueType.Boolean || rightNodeValueType != NodeValueType.Boolean, "OperandsNotBoolean", OperatorSymbols, leftNodeValueType, rightNodeValueType);

			return DetermineResultPostRightEvaluation((bool) leftNodeValue, (bool) rightNodeValue);
		}

		protected abstract bool? DetermineResultPreRightEvaluation(bool leftResult);

		protected abstract bool DetermineResultPostRightEvaluation(bool leftResult, bool rightResult);
	}
}
