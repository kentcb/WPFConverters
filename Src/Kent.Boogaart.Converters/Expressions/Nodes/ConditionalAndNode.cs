namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node to a conditional and between the left and right nodes
	internal sealed class ConditionalAndNode : ConditionalNode
	{
		protected override string OperatorSymbols
		{
			get
			{
				return "&&";
			}
		}

		public ConditionalAndNode(Node leftNode, Node rightNode)
			: base(leftNode, rightNode)
		{
		}

		protected override bool? DetermineResultPreRightEvaluation(bool leftResult)
		{
			if (!leftResult)
			{
				return false;
			}

			//must evaluate right node to determine result
			return null;
		}

		protected override bool DetermineResultPostRightEvaluation(bool leftResult, bool rightResult)
		{
			//we already know leftResult is true
			return rightResult;
		}
	}
}
