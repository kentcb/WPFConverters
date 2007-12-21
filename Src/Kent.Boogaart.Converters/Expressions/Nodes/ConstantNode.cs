using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node to hold a constant value
	internal sealed class ConstantNode<T> : Node
	{
		private readonly T _value;

		public T Value
		{
			get
			{
				return _value;
			}
		}

		public ConstantNode(T value)
		{
			Debug.Assert(value != null);
			_value = value;
		}

		public override object Evaluate(NodeEvaluationContext evaluationContext)
		{
			return _value;
		}
	}
}
