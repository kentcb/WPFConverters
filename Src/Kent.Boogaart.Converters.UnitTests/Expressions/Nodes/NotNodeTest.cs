using System.Windows;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions.Nodes
{
    public sealed class NotNodeTest : UnitTest
    {
        private NotNode notNode;

        [Fact]
        public void Evaluate_ShouldReturnUnsetValueIfOperandIsUnsetValue()
        {
            this.notNode = new NotNode(new ConstantNode<object>(DependencyProperty.UnsetValue));
            Assert.Equal(DependencyProperty.UnsetValue, this.notNode.Evaluate(NodeEvaluationContext.Empty));
        }

        [Fact]
        public void Evaluate_ShouldThrowIfNotBoolean1()
        {
            this.notNode = new NotNode(new ConstantNode<double>(1.2));
            var ex = Assert.Throws<ParseException>(() => this.notNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator '!' cannot be applied to operand of type 'Double'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldThrowIfNotBoolean2()
        {
            this.notNode = new NotNode(new ConstantNode<int>(1));
            var ex = Assert.Throws<ParseException>(() => this.notNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator '!' cannot be applied to operand of type 'Int32'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldNotGivenValue()
        {
            this.notNode = new NotNode(new ConstantNode<bool>(false));
            Assert.Equal(true, this.notNode.Evaluate(NodeEvaluationContext.Empty));

            this.notNode = new NotNode(new ConstantNode<bool>(true));
            Assert.Equal(false, this.notNode.Evaluate(NodeEvaluationContext.Empty));
        }
    }
}
