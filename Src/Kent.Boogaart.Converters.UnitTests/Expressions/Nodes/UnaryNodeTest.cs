using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions.Nodes
{
    public sealed class UnaryNodeTest : UnitTest
    {
        private MockUnaryNode unaryNode;
        private Node child;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.child = new ConstantNode<int>(0);
            this.unaryNode = new MockUnaryNode(this.child);
        }

        [Fact]
        public void Node_ShouldYieldGivenNode()
        {
            Assert.Same(this.child, this.unaryNode.Node);
        }

        #region Supporting Types

        // cannot mock because UnaryNode is internal
        private sealed class MockUnaryNode : UnaryNode
        {
            public MockUnaryNode(Node node)
                : base(node)
            {
            }

            public override object Evaluate(NodeEvaluationContext evaluationContext)
            {
                return null;
            }
        }

        #endregion
    }
}
