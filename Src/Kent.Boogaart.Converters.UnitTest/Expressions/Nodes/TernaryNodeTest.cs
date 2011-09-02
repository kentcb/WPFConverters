using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public class TernaryNodeTest : UnitTest
    {
        private MockTernaryNode _ternaryNode;
        private Node _firstNode;
        private Node _secondNode;
        private Node _thirdNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _firstNode = new ConstantNode<int>(0);
            _secondNode = new ConstantNode<int>(0);
            _thirdNode = new ConstantNode<int>(0);
            _ternaryNode = new MockTernaryNode(_firstNode, _secondNode, _thirdNode);
        }

        [Fact]
        public void First_ShouldYieldGivenNode()
        {
            Assert.Same(_firstNode, _ternaryNode.FirstNode);
        }

        [Fact]
        public void Second_ShouldYieldGivenNode()
        {
            Assert.Same(_secondNode, _ternaryNode.SecondNode);
        }

        [Fact]
        public void Third_ShouldYieldGivenNode()
        {
            Assert.Same(_thirdNode, _ternaryNode.ThirdNode);
        }

        #region Supporting Types

        //cannot mock because TernaryNode is internal
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
