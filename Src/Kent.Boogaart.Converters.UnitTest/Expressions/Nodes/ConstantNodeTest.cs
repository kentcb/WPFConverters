using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class ConstantNodeTest : UnitTest
    {
        private ConstantNode<int> intConstantNode;
        private ConstantNode<float> floatConstantNode;

        [Fact]
        public void Value_ShouldYieldAssignedValue()
        {
            this.CreateNodes(3, 3.8f);
            Assert.Equal(3, this.intConstantNode.Value);
            Assert.Equal(3.8f, this.floatConstantNode.Value);
        }

        [Fact]
        public void Evaluate_ShouldReturnAssignedValue()
        {
            this.CreateNodes(3, 3.8f);
            Assert.Equal(3, this.intConstantNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal(3.8f, this.floatConstantNode.Evaluate(NodeEvaluationContext.Empty));
        }

        #region Helper methods

        private void CreateNodes(int intConstant, float floatConstant)
        {
            this.intConstantNode = new ConstantNode<int>(intConstant);
            this.floatConstantNode = new ConstantNode<float>(floatConstant);
        }

        #endregion
    }
}
