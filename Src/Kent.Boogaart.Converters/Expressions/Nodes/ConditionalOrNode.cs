namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    // a node to a conditional or between the left and right nodes
    internal sealed class ConditionalOrNode : ConditionalBinaryNode
    {
        public ConditionalOrNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        {
        }

        protected override string OperatorSymbols
        {
            get { return "||"; }
        }

        protected override bool? DetermineResultPreRightEvaluation(bool leftResult)
        {
            if (leftResult)
            {
                return true;
            }

            // must evaluate right node to determine result
            return null;
        }

        protected override bool DetermineResultPostRightEvaluation(bool leftResult, bool rightResult)
        {
            // we already know leftResult is false
            return rightResult;
        }
    }
}
