namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    using Kent.Boogaart.HelperTrinity;
    using System.Diagnostics;
    using System.Windows;

    // a node from which shift nodes will inherit
    internal abstract class ShiftNode : BinaryNode
    {
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(ShiftNode));

        public ShiftNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        {
        }

        public override object Evaluate(NodeEvaluationContext evaluationContext)
        {
            var leftNodeValue = LeftNode.Evaluate(evaluationContext);

            if (leftNodeValue == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var rightNodeValue = RightNode.Evaluate(evaluationContext);

            if (rightNodeValue == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var leftNodeValueType = GetNodeValueType(leftNodeValue);
            var rightNodeValueType = GetNodeValueType(rightNodeValue);

            // right operand must always be Int32
            exceptionHelper.ResolveAndThrowIf(!Node.IsNumericalNodeValueType(leftNodeValueType) || rightNodeValueType != NodeValueType.Int32, "NodeValuesNotSupportedTypes", this.OperatorSymbols, leftNodeValueType, rightNodeValueType);

            switch (leftNodeValueType)
            {
                case NodeValueType.Byte:
                    return this.DoByte((byte)leftNodeValue, (int)rightNodeValue);
                case NodeValueType.Int16:
                    return this.DoInt16((short)leftNodeValue, (int)rightNodeValue);
                case NodeValueType.Int32:
                    return this.DoInt32((int)leftNodeValue, (int)rightNodeValue);
                case NodeValueType.Int64:
                    return this.DoInt64((long)leftNodeValue, (int)rightNodeValue);
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
