namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    // a node to add the left node to the right node
    internal sealed class AddNode : WideningBinaryNode
    {
        protected override string OperatorSymbols
        {
            get
            {
                return "+";
            }
        }

        public AddNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        {
        }

        protected override bool DoString(string value1, string value2, out object result)
        {
            result = value1 + value2;
            return true;
        }

        protected override bool DoByte(byte value1, byte value2, out object result)
        {
            result = value1 + value2;
            return true;
        }

        protected override bool DoInt16(short value1, short value2, out object result)
        {
            result = value1 + value2;
            return true;
        }

        protected override bool DoInt32(int value1, int value2, out object result)
        {
            result = value1 + value2;
            return true;
        }

        protected override bool DoInt64(long value1, long value2, out object result)
        {
            result = value1 + value2;
            return true;
        }

        protected override bool DoSingle(float value1, float value2, out object result)
        {
            result = value1 + value2;
            return true;
        }

        protected override bool DoDouble(double value1, double value2, out object result)
        {
            result = value1 + value2;
            return true;
        }

        protected override bool DoDecimal(decimal value1, decimal value2, out object result)
        {
            result = value1 + value2;
            return true;
        }
    }
}
