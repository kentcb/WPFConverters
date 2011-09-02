using Xunit;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class ConditionalBinaryNodeTest : UnitTest
    {
        private MockConditionalBinaryNode _conditionalBinaryNode;

        [Fact]
        public void Evaluate_ShouldThrowIfTypesArentBoolean1()
        {
            _conditionalBinaryNode = new MockConditionalBinaryNode(new ConstantNode<bool>(false), new ConstantNode<int>(0));
            var ex = Assert.Throws<ParseException>(() => _conditionalBinaryNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator 'op' cannot be applied to operands of type 'Boolean' and 'Int32'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldThrowIfTypesArentBoolean2()
        {
            _conditionalBinaryNode = new MockConditionalBinaryNode(new ConstantNode<int>(1), new ConstantNode<bool>(false));
            var ex = Assert.Throws<ParseException>(() => _conditionalBinaryNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator 'op' cannot be applied to operands of type 'Int32' and 'Boolean'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldOnlyEvaluateRightNodeIfNecessary()
        {
            _conditionalBinaryNode = new MockConditionalBinaryNode(new ConstantNode<bool>(false), new ConstantNode<bool>(false));
            _conditionalBinaryNode.PreRightEvaluationResult = true;
            Assert.Equal(true, _conditionalBinaryNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.True(_conditionalBinaryNode.PreRightEvaluationDetermineResultCalled);
            Assert.False(_conditionalBinaryNode.PostRightEvaluationDetermineResultCalled);

            _conditionalBinaryNode.PreRightEvaluationResult = null;
            _conditionalBinaryNode.PostRightEvaluationResult = true;
            Assert.Equal(true, _conditionalBinaryNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.True(_conditionalBinaryNode.PreRightEvaluationDetermineResultCalled);
            Assert.True(_conditionalBinaryNode.PostRightEvaluationDetermineResultCalled);
        }

        #region Supporting Types

        private sealed class MockConditionalBinaryNode : ConditionalBinaryNode
        {
            public bool? PreRightEvaluationResult;
            public bool PostRightEvaluationResult;
            public bool PreRightEvaluationDetermineResultCalled;
            public bool PostRightEvaluationDetermineResultCalled;

            protected override string OperatorSymbols
            {
                get
                {
                    return "op";
                }
            }

            public MockConditionalBinaryNode(Node leftNode, Node rightNode)
                : base(leftNode, rightNode)
            {
            }

            protected override bool? DetermineResultPreRightEvaluation(bool leftResult)
            {
                PreRightEvaluationDetermineResultCalled = true;
                return PreRightEvaluationResult;
            }

            protected override bool DetermineResultPostRightEvaluation(bool leftResult, bool rightResult)
            {
                PostRightEvaluationDetermineResultCalled = true;
                return PostRightEvaluationResult;
            }
        }

        #endregion
    }
}
