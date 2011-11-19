using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class ConditionalOrNodeTest : UnitTest
    {
        private ConditionalOrNode conditionalOrNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.conditionalOrNode = new ConditionalOrNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("||", GetPrivateMemberValue<string>(this.conditionalOrNode, "OperatorSymbols"));
        }

        [Fact]
        public void DetermineResultPreRightEvaluation_ShouldReturnTrueIsLeftIsTrue()
        {
            Assert.True(InvokePrivateMethod<bool?>(this.conditionalOrNode, "DetermineResultPreRightEvaluation", true).Value);
        }

        [Fact]
        public void DetermineResultPreRightEvaluation_ShouldReturnNullIsLeftIsFalse()
        {
            Assert.Null(InvokePrivateMethod<bool?>(this.conditionalOrNode, "DetermineResultPreRightEvaluation", false));
        }

        [Fact]
        public void DetermineResultPostRightEvaluation_ShouldReturnRightNodeValue()
        {
            Assert.False(InvokePrivateMethod<bool>(this.conditionalOrNode, "DetermineResultPostRightEvaluation", false, false));
            Assert.False(InvokePrivateMethod<bool>(this.conditionalOrNode, "DetermineResultPostRightEvaluation", true, false));
            Assert.True(InvokePrivateMethod<bool>(this.conditionalOrNode, "DetermineResultPostRightEvaluation", false, true));
            Assert.True(InvokePrivateMethod<bool>(this.conditionalOrNode, "DetermineResultPostRightEvaluation", true, true));
        }
    }
}
