using System;
using System.Diagnostics;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node to cast a value to another type
	internal sealed class CastNode : UnaryNode
	{
		private readonly NodeValueType _targetType;

		public CastNode(Node node, NodeValueType targetType)
			: base(node)
		{
			Debug.Assert(Enum.IsDefined(typeof(NodeValueType), targetType));
			Debug.Assert(targetType != NodeValueType.Unknown);
			_targetType = targetType;
		}

		public override object Evaluate(NodeEvaluationContext evaluationContext)
		{
			object value = Node.Evaluate(evaluationContext);
			NodeValueType nodeValueType = GetNodeValueType(value);
			bool canCast = (IsNumericalNodeValueType(nodeValueType) && IsNumericalNodeValueType(_targetType)) ||
							(nodeValueType == NodeValueType.Boolean && _targetType == NodeValueType.Boolean) ||
							(nodeValueType == NodeValueType.String && _targetType == NodeValueType.String);
			ExceptionHelper.ThrowIf(!canCast, "CannotCast", nodeValueType, _targetType);

			switch (nodeValueType)
			{
				case NodeValueType.Boolean:
					return Cast((bool) value);
				case NodeValueType.String:
					return Cast(value as string);
				case NodeValueType.Byte:
					return Cast((byte) value);
				case NodeValueType.Int16:
					return Cast((short) value);
				case NodeValueType.Int32:
					return Cast((int) value);
				case NodeValueType.Int64:
					return Cast((long) value);
				case NodeValueType.Single:
					return Cast((float) value);
				case NodeValueType.Double:
					return Cast((double) value);
				case NodeValueType.Decimal:
					return Cast((decimal) value);
			}

			Debug.Assert(false);
			return null;
		}

		private object Cast<T>(T value)
			where T : IConvertible
		{
			switch (_targetType)
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
