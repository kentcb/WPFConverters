using Xunit;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class ComplementNodeTest : UnitTest
    {
        private ComplementNode _complementNode;

        [Fact]
        public void Evaluate_ShouldThrowIfNotIntegral1()
        {
            _complementNode = new ComplementNode(new ConstantNode<bool>(true));
            var ex = Assert.Throws<ParseException>(() => _complementNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator '~' cannot be applied to operand of type 'Boolean'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldThrowIfNotIntegral2()
        {
            _complementNode = new ComplementNode(new ConstantNode<double>(1.3));
            var ex = Assert.Throws<ParseException>(() => _complementNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator '~' cannot be applied to operand of type 'Double'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldComplementGivenValue()
        {
            _complementNode = new ComplementNode(new ConstantNode<byte>(1));
            Assert.Equal(~1, _complementNode.Evaluate(NodeEvaluationContext.Empty));

            _complementNode = new ComplementNode(new ConstantNode<short>(1));
            Assert.Equal(~1, _complementNode.Evaluate(NodeEvaluationContext.Empty));

            _complementNode = new ComplementNode(new ConstantNode<int>(1));
            Assert.Equal(~1, _complementNode.Evaluate(NodeEvaluationContext.Empty));

            _complementNode = new ComplementNode(new ConstantNode<long>(1));
            Assert.Equal(~1L, _complementNode.Evaluate(NodeEvaluationContext.Empty));
        }
    }
}
