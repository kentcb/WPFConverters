namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    // a node that performs a logical or between the left and right nodes
    internal sealed class LogicalOrNode : WideningBinaryNode
    {
        public LogicalOrNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        {
        }

        protected override string OperatorSymbols
        {
            get { return "|"; }
        }

        protected override bool DoBoolean(bool value1, bool value2, out object result)
        {
            result = value1 | value2;
            return true;
        }

        protected override bool DoByte(byte value1, byte value2, out object result)
        {
            result = value1 | value2;
            return true;
        }

        protected override bool DoInt16(short value1, short value2, out object result)
        {
            result = value1 | value2;
            return true;
        }

        protected override bool DoInt32(int value1, int value2, out object result)
        {
            result = value1 | value2;
            return true;
        }

        protected override bool DoInt64(long value1, long value2, out object result)
        {
            result = value1 | value2;
            return true;
        }
    }
}
