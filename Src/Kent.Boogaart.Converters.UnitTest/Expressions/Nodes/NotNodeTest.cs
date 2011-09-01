using Xunit;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class NotNodeTest : UnitTest
    {
        private NotNode _notNode;

        [Fact]
        public void Evaluate_ShouldThrowIfNotBoolean1()
        {
            _notNode = new NotNode(new ConstantNode<double>(1.2));
            var ex = Assert.Throws<ParseException>(() => _notNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator '!' cannot be applied to operand of type 'Double'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldThrowIfNotBoolean2()
        {
            _notNode = new NotNode(new ConstantNode<int>(1));
            var ex = Assert.Throws<ParseException>(() => _notNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator '!' cannot be applied to operand of type 'Int32'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldNotGivenValue()
        {
            _notNode = new NotNode(new ConstantNode<bool>(false));
            Assert.Equal(true, _notNode.Evaluate(NodeEvaluationContext.Empty));

            _notNode = new NotNode(new ConstantNode<bool>(true));
            Assert.Equal(false, _notNode.Evaluate(NodeEvaluationContext.Empty));
        }
    }
}
