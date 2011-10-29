namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node that performs a logical xor between the left and right nodes
	internal sealed class LogicalXorNode : WideningBinaryNode
	{
		protected override string OperatorSymbols
		{
			get
			{
				return "^";
			}
		}

		public LogicalXorNode(Node leftNode, Node rightNode)
			: base(leftNode, rightNode)
		{
		}

		protected override bool DoBoolean(bool value1, bool value2, out object result)
		{
			result = value1 ^ value2;
            return true;
		}

		protected override bool DoByte(byte value1, byte value2, out object result)
		{
            result = value1 ^ value2;
            return true;
        }

		protected override bool DoInt16(short value1, short value2, out object result)
		{
            result = value1 ^ value2;
            return true;
        }

		protected override bool DoInt32(int value1, int value2, out object result)
		{
            result = value1 ^ value2;
            return true;
        }

		protected override bool DoInt64(long value1, long value2, out object result)
		{
            result = value1 ^ value2;
            return true;
        }
	}
}
