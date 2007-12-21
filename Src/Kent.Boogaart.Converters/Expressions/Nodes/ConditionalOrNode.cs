using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node to a conditional or between the left and right nodes
	internal sealed class ConditionalOrNode : ConditionalNode
	{
		protected override string OperatorSymbols
		{
			get
			{
				return "||";
			}
		}

		public ConditionalOrNode(Node leftNode, Node rightNode)
			: base(leftNode, rightNode)
		{
		}

		protected override bool? DetermineResultPreRightEvaluation(bool leftResult)
		{
			if (leftResult)
			{
				return true;
			}

			//must evaluate right node to determine result
			return null;
		}

		protected override bool DetermineResultPostRightEvaluation(bool leftResult, bool rightResult)
		{
			//we already know leftResult is false
			return rightResult;
		}
	}
}
