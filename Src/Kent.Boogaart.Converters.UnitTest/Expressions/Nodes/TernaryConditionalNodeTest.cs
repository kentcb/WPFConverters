using System.Windows;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class TernaryConditionalNodeTest : UnitTest
    {
        [Fact]
        public void Evaluate_ShouldReturnUnsetValueIfFirstOperandIsUnsetValue()
        {
            var node = new TernaryConditionalNode(new ConstantNode<object>(DependencyProperty.UnsetValue), new ConstantNode<int>(10), new ConstantNode<int>(20));
            Assert.Equal(DependencyProperty.UnsetValue, node.Evaluate(NodeEvaluationContext.Empty));
        }

        [Fact]
        public void Evaluate_ShouldThrowIfFirstNodeTypeIsNotBoolean()
        {
            var node = new TernaryConditionalNode(new ConstantNode<int>(0), new ConstantNode<int>(0), new ConstantNode<int>(0));
            var ex = Assert.Throws<ParseException>(() => node.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator '?' requires that the first node be of type Boolean, but it is of type 'Int32'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldReturnSecondValueIfFirstValueIsTrue()
        {
            var node = new TernaryConditionalNode(new ConstantNode<bool>(true), new ConstantNode<int>(10), new ConstantNode<int>(20));
            var result = node.Evaluate(NodeEvaluationContext.Empty);

            Assert.Equal(10, result);
        }

        [Fact]
        public void Evaluate_ShouldReturnThirdValueIfFirstValueIsFalse()
        {
            var node = new TernaryConditionalNode(new ConstantNode<bool>(false), new ConstantNode<int>(10), new ConstantNode<int>(20));
            var result = node.Evaluate(NodeEvaluationContext.Empty);

            Assert.Equal(20, result);
        }
    }
}
