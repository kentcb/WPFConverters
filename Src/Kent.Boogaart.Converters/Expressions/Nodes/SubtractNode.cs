namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node to subtract the right node from the left node
	internal sealed class SubtractNode : WideningBinaryNode
	{
		protected override string OperatorSymbols
		{
			get
			{
				return "-";
			}
		}

		public SubtractNode(Node leftNode, Node rightNode)
			: base(leftNode, rightNode)
		{
		}

		protected override bool IsSupported(NodeValueType leftNodeValueType, NodeValueType rightNodeValueType)
		{
			return IsNumericalNodeValueType(leftNodeValueType) && IsNumericalNodeValueType(rightNodeValueType);
		}

		protected override object DoByte(byte value1, byte value2)
		{
			return value1 - value2;
		}

		protected override object DoInt16(short value1, short value2)
		{
			return value1 - value2;
		}

		protected override object DoInt32(int value1, int value2)
		{
			return value1 - value2;
		}

		protected override object DoInt64(long value1, long value2)
		{
			return value1 - value2;
		}

		protected override object DoSingle(float value1, float value2)
		{
			return value1 - value2;
		}

		protected override object DoDouble(double value1, double value2)
		{
			return value1 - value2;
		}

		protected override object DoDecimal(decimal value1, decimal value2)
		{
			return value1 - value2;
		}
	}
}
