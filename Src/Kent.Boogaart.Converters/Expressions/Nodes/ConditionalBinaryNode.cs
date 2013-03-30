namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    using Kent.Boogaart.HelperTrinity;
    using System.Windows;

    // a node from which conditional binary nodes will inherit
    internal abstract class ConditionalBinaryNode : BinaryNode
    {
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(ConditionalBinaryNode));

        protected ConditionalBinaryNode(Node leftNode, Node rightNode)
            : base(leftNode, rightNode)
        {
        }

        public sealed override object Evaluate(NodeEvaluationContext evaluationContext)
        {
            var leftNodeValue = LeftNode.Evaluate(evaluationContext);

            if (leftNodeValue == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var leftNodeValueType = GetNodeValueType(leftNodeValue);

            if (leftNodeValueType == NodeValueType.Boolean)
            {
                // give base a chance to yield a result without evaluating the right node
                var result = this.DetermineResultPreRightEvaluation((bool)leftNodeValue);

                if (result.HasValue)
                {
                    return result.Value;
                }
            }

            var rightNodeValue = RightNode.Evaluate(evaluationContext);

            if (rightNodeValue == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var rightNodeValueType = GetNodeValueType(rightNodeValue);
            exceptionHelper.ResolveAndThrowIf(leftNodeValueType != NodeValueType.Boolean || rightNodeValueType != NodeValueType.Boolean, "OperandsNotBoolean", this.OperatorSymbols, leftNodeValueType, rightNodeValueType);

            return this.DetermineResultPostRightEvaluation((bool)leftNodeValue, (bool)rightNodeValue);
        }

        protected abstract bool? DetermineResultPreRightEvaluation(bool leftResult);

        protected abstract bool DetermineResultPostRightEvaluation(bool leftResult, bool rightResult);
    }
}
