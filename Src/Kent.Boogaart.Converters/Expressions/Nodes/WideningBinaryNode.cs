using System;
using System.Diagnostics;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node that automatically widens one value to match the width of the other if necessary
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

			//base class determines whether the operation is supported
			exceptionHelper.ResolveAndThrowIf(!IsSupported(leftNodeValueType, rightNodeValueType), "OperatorNotSupportedWithOperands", OperatorSymbols, leftNodeValueType, rightNodeValueType);

			//determine the type to which we will need to widen any narrower value
			NodeValueType maxNodeValueType = (NodeValueType) Math.Max((int) leftNodeValueType, (int) rightNodeValueType);

			//we have to convert rather than just cast because the value is boxed
			IConvertible convertibleLeftNodeValue = leftNodeValue as IConvertible;
			IConvertible convertibleRightNodeValue = rightNodeValue as IConvertible;
			Debug.Assert(convertibleLeftNodeValue != null || maxNodeValueType == NodeValueType.String);
			Debug.Assert(convertibleRightNodeValue != null || maxNodeValueType == NodeValueType.String);

			switch (maxNodeValueType)
			{
				case NodeValueType.String:
					return DoString(convertibleLeftNodeValue == null ? null : convertibleLeftNodeValue.ToString(null), convertibleRightNodeValue == null ? null : convertibleRightNodeValue.ToString(null));
				case NodeValueType.Boolean:
					return DoBoolean(convertibleLeftNodeValue.ToBoolean(null), convertibleRightNodeValue.ToBoolean(null));
				case NodeValueType.Byte:
					return DoByte(convertibleLeftNodeValue.ToByte(null), convertibleRightNodeValue.ToByte(null));
				case NodeValueType.Int16:
					return DoInt16(convertibleLeftNodeValue.ToInt16(null), convertibleRightNodeValue.ToInt16(null));
				case NodeValueType.Int32:
					return DoInt32(convertibleLeftNodeValue.ToInt32(null), convertibleRightNodeValue.ToInt32(null));
				case NodeValueType.Int64:
					return DoInt64(convertibleLeftNodeValue.ToInt64(null), convertibleRightNodeValue.ToInt64(null));
				case NodeValueType.Single:
					return DoSingle(convertibleLeftNodeValue.ToSingle(null), convertibleRightNodeValue.ToSingle(null));
				case NodeValueType.Double:
					return DoDouble(convertibleLeftNodeValue.ToDouble(null), convertibleRightNodeValue.ToDouble(null));
				case NodeValueType.Decimal:
					return DoDecimal(convertibleLeftNodeValue.ToDecimal(null), convertibleRightNodeValue.ToDecimal(null));
			}

			Debug.Assert(false);
			return null;
		}

		protected abstract bool IsSupported(NodeValueType leftNodeValueType, NodeValueType rightNodeValueType);

		protected virtual object DoString(string value1, string value2)
		{
			Debug.Assert(false);
			return null;
		}

		protected virtual object DoBoolean(bool value1, bool value2)
		{
			Debug.Assert(false);
			return null;
		}

		protected virtual object DoByte(byte value1, byte value2)
		{
			Debug.Assert(false);
			return null;
		}

		protected virtual object DoInt16(short value1, short value2)
		{
			Debug.Assert(false);
			return null;
		}

		protected virtual object DoInt32(int value1, int value2)
		{
			Debug.Assert(false);
			return null;
		}

		protected virtual object DoInt64(long value1, long value2)
		{
			Debug.Assert(false);
			return null;
		}

		protected virtual object DoSingle(float value1, float value2)
		{
			Debug.Assert(false);
			return null;
		}

		protected virtual object DoDouble(double value1, double value2)
		{
			Debug.Assert(false);
			return null;
		}

		protected virtual object DoDecimal(decimal value1, decimal value2)
		{
			Debug.Assert(false);
			return null;
		}
	}
}
