using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node from which shift nodes will inherit
	internal abstract class ShiftNode : BinaryNode
	{
		public ShiftNode(Node leftNode, Node rightNode)
			: base(leftNode, rightNode)
		{
		}

		public override object Evaluate(NodeEvaluationContext evaluationContext)
		{
			object leftNodeValue = LeftNode.Evaluate(evaluationContext);
			object rightNodeValue = RightNode.Evaluate(evaluationContext);
			NodeValueType leftNodeValueType = Node.GetNodeValueType(leftNodeValue);
			NodeValueType rightNodeValueType = Node.GetNodeValueType(rightNodeValue);
			//right operand must always be Int32
			ExceptionHelper.ThrowIf(!Node.IsNumericalNodeValueType(leftNodeValueType) || rightNodeValueType != NodeValueType.Int32, "NodeValuesNotSupportedTypes", OperatorSymbols, leftNodeValueType, rightNodeValueType);

			switch (leftNodeValueType)
			{
				case NodeValueType.Byte:
					return DoByte((byte) leftNodeValue, (int) rightNodeValue);
				case NodeValueType.Int16:
					return DoInt16((short) leftNodeValue, (int) rightNodeValue);
				case NodeValueType.Int32:
					return DoInt32((int) leftNodeValue, (int) rightNodeValue);
				case NodeValueType.Int64:
					return DoInt64((long) leftNodeValue, (int) rightNodeValue);
			}

			Debug.Assert(false);
			return null;
		}

		protected abstract int DoByte(byte value1, int value2);

		protected abstract int DoInt16(short value1, int value2);

		protected abstract int DoInt32(int value1, int value2);

		protected abstract long DoInt64(long value1, int value2);
	}
}
