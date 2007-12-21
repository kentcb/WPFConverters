using System;
using System.Collections.Generic;
using System.Text;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node that performs a logical and between the left and right nodes
	internal sealed class LogicalAndNode : WideningBinaryNode
	{
		protected override string OperatorSymbols
		{
			get
			{
				return "&";
			}
		}

		public LogicalAndNode(Node leftNode, Node rightNode)
			: base(leftNode, rightNode)
		{
		}

		protected override bool IsSupported(NodeValueType leftNodeValueType, NodeValueType rightNodeValueType)
		{
			//both boolean or both integral
			return (leftNodeValueType == NodeValueType.Boolean && rightNodeValueType == NodeValueType.Boolean) || (Node.IsIntegralNodeValueType(leftNodeValueType) && Node.IsIntegralNodeValueType(rightNodeValueType));
		}

		protected override object DoBoolean(bool value1, bool value2)
		{
			return value1 & value2;
		}

		protected override object DoByte(byte value1, byte value2)
		{
			return value1 & value2;
		}

		protected override object DoInt16(short value1, short value2)
		{
			return value1 & value2;
		}

		protected override object DoInt32(int value1, int value2)
		{
			return value1 & value2;
		}

		protected override object DoInt64(long value1, long value2)
		{
			return value1 & value2;
		}
	}
}
