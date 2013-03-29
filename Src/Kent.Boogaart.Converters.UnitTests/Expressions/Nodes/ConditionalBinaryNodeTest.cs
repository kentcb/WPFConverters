using System.Windows;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions.Nodes
{
    public sealed class ConditionalBinaryNodeTest : UnitTest
    {
        private MockConditionalBinaryNode conditionalBinaryNode;

        [Fact]
        public void Evaluate_ShouldReturnUnsetValueIfAnyOperandIsUnsetValue()
        {
            this.conditionalBinaryNode = new MockConditionalBinaryNode(new ConstantNode<object>(DependencyProperty.UnsetValue), new ConstantNode<bool>(false));
            Assert.Equal(DependencyProperty.UnsetValue, this.conditionalBinaryNode.Evaluate(NodeEvaluationContext.Empty));

            this.conditionalBinaryNode = new MockConditionalBinaryNode(new ConstantNode<bool>(false), new ConstantNode<object>(DependencyProperty.UnsetValue));
            Assert.Equal(DependencyProperty.UnsetValue, this.conditionalBinaryNode.Evaluate(NodeEvaluationContext.Empty));
        }

        [Fact]
        public void Evaluate_ShouldThrowIfTypesArentBoolean1()
        {
            this.conditionalBinaryNode = new MockConditionalBinaryNode(new ConstantNode<bool>(false), new ConstantNode<int>(0));
            var ex = Assert.Throws<ParseException>(() => this.conditionalBinaryNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator 'op' cannot be applied to operands of type 'Boolean' and 'Int32' because at least one is non-boolean.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldThrowIfTypesArentBoolean2()
        {
            this.conditionalBinaryNode = new MockConditionalBinaryNode(new ConstantNode<int>(1), new ConstantNode<bool>(false));
            var ex = Assert.Throws<ParseException>(() => this.conditionalBinaryNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator 'op' cannot be applied to operands of type 'Int32' and 'Boolean' because at least one is non-boolean.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldOnlyEvaluateRightNodeIfNecessary()
        {
            this.conditionalBinaryNode = new MockConditionalBinaryNode(new ConstantNode<bool>(false), new ConstantNode<bool>(false));
            this.conditionalBinaryNode.PreRightEvaluationResult = true;
            Assert.Equal(true, this.conditionalBinaryNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.True(this.conditionalBinaryNode.PreRightEvaluationDetermineResultCalled);
            Assert.False(this.conditionalBinaryNode.PostRightEvaluationDetermineResultCalled);

            this.conditionalBinaryNode.PreRightEvaluationResult = null;
            this.conditionalBinaryNode.PostRightEvaluationResult = true;
            Assert.Equal(true, this.conditionalBinaryNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.True(this.conditionalBinaryNode.PreRightEvaluationDetermineResultCalled);
            Assert.True(this.conditionalBinaryNode.PostRightEvaluationDetermineResultCalled);
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
                get { return "op"; }
            }

            public MockConditionalBinaryNode(Node leftNode, Node rightNode)
                : base(leftNode, rightNode)
            {
            }

            protected override bool? DetermineResultPreRightEvaluation(bool leftResult)
            {
                this.PreRightEvaluationDetermineResultCalled = true;
                return this.PreRightEvaluationResult;
            }

            protected override bool DetermineResultPostRightEvaluation(bool leftResult, bool rightResult)
            {
                this.PostRightEvaluationDetermineResultCalled = true;
                return this.PostRightEvaluationResult;
            }
        }

        #endregion
    }
}
