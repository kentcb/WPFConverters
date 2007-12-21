using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node to check whether the left node is not equal to the right node
	internal sealed class InequalityNode : WideningBinaryNode
	{
		protected override string OperatorSymbols
		{
			get
			{
				return "!=";
			}
		}

		public InequalityNode(Node leftNode, Node rightNode)
			: base(leftNode, rightNode)
		{
		}

		protected override bool IsSupported(NodeValueType leftNodeValueType, NodeValueType rightNodeValueType)
		{
			if (Node.IsNumericalNodeValueType(rightNodeValueType) && Node.IsNumericalNodeValueType(leftNodeValueType))
			{
				return true;
			}

			return ((leftNodeValueType == NodeValueType.String || leftNodeValueType == NodeValueType.Null) &&
					(rightNodeValueType == NodeValueType.String || rightNodeValueType == NodeValueType.Null));
		}

		protected override object DoString(string value1, string value2)
		{
			return value1 != value2;
		}

		protected override object DoBoolean(bool value1, bool value2)
		{
			return value1 != value2;
		}

		protected override object DoByte(byte value1, byte value2)
		{
			return value1 != value2;
		}

		protected override object DoInt16(short value1, short value2)
		{
			return value1 != value2;
		}

		protected override object DoInt32(int value1, int value2)
		{
			return value1 != value2;
		}

		protected override object DoInt64(long value1, long value2)
		{
			return value1 != value2;
		}

		protected override object DoSingle(float value1, float value2)
		{
			return value1 != value2;
		}

		protected override object DoDouble(double value1, double value2)
		{
			return value1 != value2;
		}

		protected override object DoDecimal(decimal value1, decimal value2)
		{
			return value1 != value2;
		}
	}
}
