using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//base class for all AST nodes.
	internal abstract class Node
	{
		public abstract object Evaluate(NodeEvaluationContext evaluationContext);

		public static NodeValueType GetNodeValueType(object value)
		{
			if (value == null)
			{
				return NodeValueType.Null;
			}
			else if (value is bool)
			{
				return NodeValueType.Boolean;
			}
			else if (value is string)
			{
				return NodeValueType.String;
			}
			else if (value is byte)
			{
				return NodeValueType.Byte;
			}
			else if (value is short)
			{
				return NodeValueType.Int16;
			}
			else if (value is int)
			{
				return NodeValueType.Int32;
			}
			else if (value is long)
			{
				return NodeValueType.Int64;
			}
			else if (value is float)
			{
				return NodeValueType.Single;
			}
			else if (value is double)
			{
				return NodeValueType.Double;
			}
			else if (value is decimal)
			{
				return NodeValueType.Decimal;
			}

			return NodeValueType.Unknown;
		}

		public static bool IsNumericalNodeValueType(NodeValueType nodeValueType)
		{
			Debug.Assert(Enum.IsDefined(typeof(NodeValueType), nodeValueType));
			return nodeValueType >= NodeValueType.Byte;
		}

		public static bool IsIntegralNodeValueType(NodeValueType nodeValueType)
		{
			Debug.Assert(Enum.IsDefined(typeof(NodeValueType), nodeValueType));
			return (nodeValueType >= NodeValueType.Byte) && (nodeValueType <= NodeValueType.Int64);
		}
	}
}
