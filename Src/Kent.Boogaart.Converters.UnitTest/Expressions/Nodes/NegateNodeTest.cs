using System.Windows;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class NegateNodeTest : UnitTest
    {
        private NegateNode negateNode;

        [Fact]
        public void Evaluate_ShouldReturnUnsetValueIfOperandIsUnsetValue()
        {
            this.negateNode = new NegateNode(new ConstantNode<object>(DependencyProperty.UnsetValue));
            Assert.Equal(DependencyProperty.UnsetValue, this.negateNode.Evaluate(NodeEvaluationContext.Empty));
        }

        [Fact]
        public void Evaluate_ShouldThrowIfCannotNegateValue1()
        {
            this.negateNode = new NegateNode(new ConstantNode<string>(string.Empty));
            var ex = Assert.Throws<ParseException>(() => this.negateNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator '~' cannot be applied to operand of type 'String'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldThrowIfCannotNegateValue2()
        {
            this.negateNode = new NegateNode(new ConstantNode<bool>(true));
            var ex = Assert.Throws<ParseException>(() => this.negateNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator '~' cannot be applied to operand of type 'Boolean'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldNegateChildsValue()
        {
            this.negateNode = new NegateNode(new ConstantNode<byte>(3));
            Assert.Equal(-3, this.negateNode.Evaluate(NodeEvaluationContext.Empty));

            this.negateNode = new NegateNode(new ConstantNode<short>(3));
            Assert.Equal(-3, this.negateNode.Evaluate(NodeEvaluationContext.Empty));

            this.negateNode = new NegateNode(new ConstantNode<int>(3));
            Assert.Equal(-3, this.negateNode.Evaluate(NodeEvaluationContext.Empty));

            this.negateNode = new NegateNode(new ConstantNode<long>(3));
            Assert.Equal(-3L, this.negateNode.Evaluate(NodeEvaluationContext.Empty));

            this.negateNode = new NegateNode(new ConstantNode<float>(3));
            Assert.Equal(-3f, this.negateNode.Evaluate(NodeEvaluationContext.Empty));

            this.negateNode = new NegateNode(new ConstantNode<double>(3));
            Assert.Equal(-3d, this.negateNode.Evaluate(NodeEvaluationContext.Empty));

            this.negateNode = new NegateNode(new ConstantNode<decimal>(3));
            Assert.Equal(-3M, this.negateNode.Evaluate(NodeEvaluationContext.Empty));

            this.negateNode = new NegateNode(new ConstantNode<int>(-3));
            Assert.Equal(3, this.negateNode.Evaluate(NodeEvaluationContext.Empty));
        }
    }
}
