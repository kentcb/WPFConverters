namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    // a node to determine whether the left node is less than or equal to the right node
    internal sealed class LessThanOrEqualNode : WideningBinaryNode
    {
        public LessThanOrEqualNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        {
        }

        protected override string OperatorSymbols
        {
            get { return "<="; }
        }

        protected override bool DoByte(byte value1, byte value2, out object result)
        {
            result = value1 <= value2;
            return true;
        }

        protected override bool DoInt16(short value1, short value2, out object result)
        {
            result = value1 <= value2;
            return true;
        }

        protected override bool DoInt32(int value1, int value2, out object result)
        {
            result = value1 <= value2;
            return true;
        }

        protected override bool DoInt64(long value1, long value2, out object result)
        {
            result = value1 <= value2;
            return true;
        }

        protected override bool DoSingle(float value1, float value2, out object result)
        {
            result = value1 <= value2;
            return true;
        }

        protected override bool DoDouble(double value1, double value2, out object result)
        {
            result = value1 <= value2;
            return true;
        }

        protected override bool DoDecimal(decimal value1, decimal value2, out object result)
        {
            result = value1 <= value2;
            return true;
        }
    }
}
