using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public class TernaryNodeTest : UnitTest
    {
        private MockTernaryNode ternaryNode;
        private Node firstNode;
        private Node secondNode;
        private Node thirdNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.firstNode = new ConstantNode<int>(0);
            this.secondNode = new ConstantNode<int>(0);
            this.thirdNode = new ConstantNode<int>(0);
            this.ternaryNode = new MockTernaryNode(this.firstNode, this.secondNode, this.thirdNode);
        }

        [Fact]
        public void First_ShouldYieldGivenNode()
        {
            Assert.Same(this.firstNode, this.ternaryNode.FirstNode);
        }

        [Fact]
        public void Second_ShouldYieldGivenNode()
        {
            Assert.Same(this.secondNode, this.ternaryNode.SecondNode);
        }

        [Fact]
        public void Third_ShouldYieldGivenNode()
        {
            Assert.Same(this.thirdNode, this.ternaryNode.ThirdNode);
        }

        #region Supporting Types

        // cannot mock because TernaryNode is internal
        private sealed class MockTernaryNode : TernaryNode
        {
            protected override string OperatorSymbols
            {
                get
                {
                    return "op";
                }
            }

            public MockTernaryNode(Node firstNode, Node secondNode, Node thirdNode)
                : base(firstNode, secondNode, thirdNode)
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
