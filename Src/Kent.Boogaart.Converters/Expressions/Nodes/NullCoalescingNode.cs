namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    internal sealed class NullCoalescingNode : BinaryNode
    {
        protected override string OperatorSymbols
        {
            get
            { 
                return "??";
            }
        }

        public NullCoalescingNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        {
        }

        public override object Evaluate(NodeEvaluationContext evaluationContext)
        {
            var leftNodeValue = LeftNode.Evaluate(evaluationContext);

            if (leftNodeValue != null)
            {
                return leftNodeValue;
            }

            return RightNode.Evaluate(evaluationContext);
        }
    }
}
