namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    using Kent.Boogaart.HelperTrinity;
    using System;
    using System.Diagnostics;
    using System.Windows;

    // a node to cast a value to another type
    internal sealed class CastNode : UnaryNode
    {
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(CastNode));
        private readonly NodeValueType targetType;

        public CastNode(Node node, NodeValueType targetType)
            : base(node)
        {
            Debug.Assert(Enum.IsDefined(typeof(NodeValueType), targetType));
            Debug.Assert(targetType != NodeValueType.Unknown);
            this.targetType = targetType;
        }

        public override object Evaluate(NodeEvaluationContext evaluationContext)
        {
            var value = Node.Evaluate(evaluationContext);

            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var nodeValueType = GetNodeValueType(value);
            var canCast = (IsNumericalNodeValueType(nodeValueType) && IsNumericalNodeValueType(this.targetType)) ||
                          (nodeValueType == NodeValueType.Boolean && this.targetType == NodeValueType.Boolean) ||
                          (nodeValueType == NodeValueType.String && this.targetType == NodeValueType.String);
            exceptionHelper.ResolveAndThrowIf(!canCast, "CannotCast", nodeValueType, this.targetType);

            switch (nodeValueType)
            {
                case NodeValueType.Boolean:
                    return this.Cast((bool)value);
                case NodeValueType.String:
                    return this.Cast(value as string);
                case NodeValueType.Byte:
                    return this.Cast((byte)value);
                case NodeValueType.Int16:
                    return this.Cast((short)value);
                case NodeValueType.Int32:
                    return this.Cast((int)value);
                case NodeValueType.Int64:
                    return this.Cast((long)value);
                case NodeValueType.Single:
                    return this.Cast((float)value);
                case NodeValueType.Double:
                    return this.Cast((double)value);
                case NodeValueType.Decimal:
                    return this.Cast((decimal)value);
            }

            Debug.Assert(false);
            return null;
        }

        private object Cast<T>(T value)
            where T : IConvertible
        {
            switch (this.targetType)
            {
                case NodeValueType.Boolean:
                    return value.ToBoolean(null);
                case NodeValueType.String:
                    return value.ToString(null);
                case NodeValueType.Byte:
                    return value.ToByte(null);
                case NodeValueType.Int16:
                    return value.ToInt16(null);
                case NodeValueType.Int32:
                    return value.ToInt32(null);
                case NodeValueType.Int64:
                    return value.ToInt64(null);
                case NodeValueType.Single:
                    return value.ToSingle(null);
                case NodeValueType.Double:
                    return value.ToDouble(null);
                case NodeValueType.Decimal:
                    return value.ToDecimal(null);
            }

            Debug.Assert(false);
            return null;
        }
    }
}
