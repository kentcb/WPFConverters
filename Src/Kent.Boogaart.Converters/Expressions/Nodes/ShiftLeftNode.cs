namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    // a node to shift an integral value left
    internal sealed class ShiftLeftNode : ShiftNode
    {
        public ShiftLeftNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        {
        }

        protected override string OperatorSymbols
        {
            get { return "<<"; }
        }

        protected override int DoByte(byte value1, int value2)
        {
            return value1 << value2;
        }

        protected override int DoInt16(short value1, int value2)
        {
            return value1 << value2;
        }

        protected override int DoInt32(int value1, int value2)
        {
            return value1 << value2;
        }

        protected override long DoInt64(long value1, int value2)
        {
            return value1 << value2;
        }
    }
}
