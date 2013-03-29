using System.Windows;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions.Nodes
{
    public sealed class ComplementNodeTest : UnitTest
    {
        private ComplementNode complementNode;

        [Fact]
        public void Evaluate_ShouldReturnUnsetValueIfOperandIsUnsetValue()
        {
            this.complementNode = new ComplementNode(new ConstantNode<object>(DependencyProperty.UnsetValue));
            Assert.Equal(DependencyProperty.UnsetValue, this.complementNode.Evaluate(NodeEvaluationContext.Empty));
        }

        [Fact]
        public void Evaluate_ShouldThrowIfNotIntegral1()
        {
            this.complementNode = new ComplementNode(new ConstantNode<bool>(true));
            var ex = Assert.Throws<ParseException>(() => this.complementNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator '~' cannot be applied to operand of type 'Boolean'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldThrowIfNotIntegral2()
        {
            this.complementNode = new ComplementNode(new ConstantNode<double>(1.3));
            var ex = Assert.Throws<ParseException>(() => this.complementNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator '~' cannot be applied to operand of type 'Double'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldComplementGivenValue()
        {
            this.complementNode = new ComplementNode(new ConstantNode<byte>(1));
            Assert.Equal(~1, this.complementNode.Evaluate(NodeEvaluationContext.Empty));

            this.complementNode = new ComplementNode(new ConstantNode<short>(1));
            Assert.Equal(~1, this.complementNode.Evaluate(NodeEvaluationContext.Empty));

            this.complementNode = new ComplementNode(new ConstantNode<int>(1));
            Assert.Equal(~1, this.complementNode.Evaluate(NodeEvaluationContext.Empty));

            this.complementNode = new ComplementNode(new ConstantNode<long>(1));
            Assert.Equal(~1L, this.complementNode.Evaluate(NodeEvaluationContext.Empty));
        }
    }
}
