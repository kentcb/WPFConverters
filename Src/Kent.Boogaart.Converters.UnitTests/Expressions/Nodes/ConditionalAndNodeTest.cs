using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions.Nodes
{
    public sealed class ConditionalAndNodeTest : UnitTest
    {
        private MockNode leftNode;
        private MockNode rightNode;
        private ConditionalAndNode conditionalAndNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.leftNode = new MockNode();
            this.rightNode = new MockNode();
            this.conditionalAndNode = new ConditionalAndNode(this.leftNode, this.rightNode);
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("&&", GetPrivateMemberValue<string>(this.conditionalAndNode, "OperatorSymbols"));
        }

        [Fact]
        public void DetermineResultPreRightEvaluation_ShouldReturnFalseIsLeftIsFalse()
        {
            Assert.False(InvokePrivateMethod<bool?>(this.conditionalAndNode, "DetermineResultPreRightEvaluation", false).Value);
        }

        [Fact]
        public void DetermineResultPreRightEvaluation_ShouldReturnNullIsLeftIsTrue()
        {
            Assert.Null(InvokePrivateMethod<bool?>(this.conditionalAndNode, "DetermineResultPreRightEvaluation", true));
        }

        [Fact]
        public void DetermineResultPostRightEvaluation_ShouldReturnRightNodeValue()
        {
            Assert.False(InvokePrivateMethod<bool>(this.conditionalAndNode, "DetermineResultPostRightEvaluation", false, false));
            Assert.False(InvokePrivateMethod<bool>(this.conditionalAndNode, "DetermineResultPostRightEvaluation", true, false));
            Assert.True(InvokePrivateMethod<bool>(this.conditionalAndNode, "DetermineResultPostRightEvaluation", false, true));
            Assert.True(InvokePrivateMethod<bool>(this.conditionalAndNode, "DetermineResultPostRightEvaluation", true, true));
        }

        #region Supporting Types

        private sealed class MockNode : Node
        {
            public bool EvaluateTo;
            public bool EvaluateCalled;

            public override object Evaluate(NodeEvaluationContext evaluationContext)
            {
                this.EvaluateCalled = true;
                return this.EvaluateTo;
            }
        }

        #endregion
    }
}
