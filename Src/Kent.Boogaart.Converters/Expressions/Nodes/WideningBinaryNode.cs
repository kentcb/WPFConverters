using System;
using System.Diagnostics;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    //a binary node that automatically widens one value to match the width of the other if necessary
    internal abstract class WideningBinaryNode : BinaryNode
    {
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(WideningBinaryNode));

        protected WideningBinaryNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        {
        }
        
        public sealed override object Evaluate(NodeEvaluationContext evaluationContext)
        {
            object leftNodeValue = LeftNode.Evaluate(evaluationContext);
            object rightNodeValue = RightNode.Evaluate(evaluationContext);
            NodeValueType leftNodeValueType = GetNodeValueType(leftNodeValue);
            NodeValueType rightNodeValueType = GetNodeValueType(rightNodeValue);

            //determine the type to which we will need to widen any narrower value
            NodeValueType maxNodeValueType = (NodeValueType) Math.Max((int) leftNodeValueType, (int) rightNodeValueType);

            //we have to convert rather than just cast because the value is boxed
            IConvertible convertibleLeftNodeValue = leftNodeValue as IConvertible;
            IConvertible convertibleRightNodeValue = rightNodeValue as IConvertible;
            Debug.Assert(convertibleLeftNodeValue != null || maxNodeValueType == NodeValueType.String);
            Debug.Assert(convertibleRightNodeValue != null || maxNodeValueType == NodeValueType.String);

            var succeeded = false;
            object result = null;

            switch (maxNodeValueType)
            {
                case NodeValueType.String:
                    succeeded = DoString(convertibleLeftNodeValue == null ? null : convertibleLeftNodeValue.ToString(null), convertibleRightNodeValue == null ? null : convertibleRightNodeValue.ToString(null), out result);
                    break;
                case NodeValueType.Boolean:
                    succeeded = DoBoolean(convertibleLeftNodeValue.ToBoolean(null), convertibleRightNodeValue.ToBoolean(null), out result);
                    break;
                case NodeValueType.Byte:
                    succeeded = DoByte(convertibleLeftNodeValue.ToByte(null), convertibleRightNodeValue.ToByte(null), out result);
                    break;
                case NodeValueType.Int16:
                    succeeded = DoInt16(convertibleLeftNodeValue.ToInt16(null), convertibleRightNodeValue.ToInt16(null), out result);
                    break;
                case NodeValueType.Int32:
                    succeeded = DoInt32(convertibleLeftNodeValue.ToInt32(null), convertibleRightNodeValue.ToInt32(null), out result);
                    break;
                case NodeValueType.Int64:
                    succeeded = DoInt64(convertibleLeftNodeValue.ToInt64(null), convertibleRightNodeValue.ToInt64(null), out result);
                    break;
                case NodeValueType.Single:
                    succeeded = DoSingle(convertibleLeftNodeValue.ToSingle(null), convertibleRightNodeValue.ToSingle(null), out result);
                    break;
                case NodeValueType.Double:
                    succeeded = DoDouble(convertibleLeftNodeValue.ToDouble(null), convertibleRightNodeValue.ToDouble(null), out result);
                    break;
                case NodeValueType.Decimal:
                    succeeded = DoDecimal(convertibleLeftNodeValue.ToDecimal(null), convertibleRightNodeValue.ToDecimal(null), out result);
                    break;
                case NodeValueType.ValueType:
                    succeeded = DoValueType(leftNodeValue, rightNodeValue, out result);
                    break;
                case NodeValueType.ReferenceType:
                    succeeded = DoReferenceType(leftNodeValue, rightNodeValue, out result);
                    break;
            }

            exceptionHelper.ResolveAndThrowIf(!succeeded, "OperatorNotSupportedWithOperands", OperatorSymbols, leftNodeValueType, rightNodeValueType);
            return result;
        }

        protected virtual bool DoString(string value1, string value2, out object result)
        {
            result = null;
            return false;
        }

        protected virtual bool DoBoolean(bool value1, bool value2, out object result)
        {
            result = null;
            return false;
        }

        protected virtual bool DoByte(byte value1, byte value2, out object result)
        {
            result = null;
            return false;
        }

        protected virtual bool DoInt16(short value1, short value2, out object result)
        {
            result = null;
            return false;
        }

        protected virtual bool DoInt32(int value1, int value2, out object result)
        {
            result = null;
            return false;
        }

        protected virtual bool DoInt64(long value1, long value2, out object result)
        {
            result = null;
            return false;
        }

        protected virtual bool DoSingle(float value1, float value2, out object result)
        {
            result = null;
            return false;
        }

        protected virtual bool DoDouble(double value1, double value2, out object result)
        {
            result = null;
            return false;
        }

        protected virtual bool DoDecimal(decimal value1, decimal value2, out object result)
        {
            result = null;
            return false;
        }

        protected virtual bool DoValueType(object value1, object value2, out object result)
        {
            result = null;
            return false;
        }

        protected virtual bool DoReferenceType(object value1, object value2, out object result)
        {
            result = null;
            return false;
        }
    }
}
