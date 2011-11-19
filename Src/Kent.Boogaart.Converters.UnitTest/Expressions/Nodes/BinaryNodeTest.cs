using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class BinaryNodeTest : UnitTest
    {
        private MockBinaryNode binaryNode;
        private Node leftNode;
        private Node rightNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.leftNode = new ConstantNode<int>(0);
            this.rightNode = new ConstantNode<int>(0);
            this.binaryNode = new MockBinaryNode(this.leftNode, this.rightNode);
        }

        [Fact]
        public void Left_ShouldYieldGivenNode()
        {
            Assert.Same(this.leftNode, this.binaryNode.LeftNode);
        }

        [Fact]
        public void Right_ShouldYieldGivenNode()
        {
            Assert.Same(this.rightNode, this.binaryNode.RightNode);
        }

        #region Supporting Types

        // cannot mock because BinaryNode is internal
        private sealed class MockBinaryNode : BinaryNode
        {
            protected override string OperatorSymbols
            {
                get { return "op"; }
            }

            public MockBinaryNode(Node leftNode, Node rightNode)
                : base(leftNode, rightNode)
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
