using Xunit;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class NegateNodeTest : UnitTest
    {
        private NegateNode _negation;

        [Fact]
        public void Evaluate_ShouldThrowIfCannotNegateValue1()
        {
            _negation = new NegateNode(new ConstantNode<string>(""));
            var ex = Assert.Throws<ParseException>(() => _negation.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator '~' cannot be applied to operand of type 'String'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldThrowIfCannotNegateValue2()
        {
            _negation = new NegateNode(new ConstantNode<bool>(true));
            var ex = Assert.Throws<ParseException>(() => _negation.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator '~' cannot be applied to operand of type 'Boolean'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldNegateChildsValue()
        {
            _negation = new NegateNode(new ConstantNode<byte>(3));
            Assert.Equal(-3, _negation.Evaluate(NodeEvaluationContext.Empty));

            _negation = new NegateNode(new ConstantNode<short>(3));
            Assert.Equal(-3, _negation.Evaluate(NodeEvaluationContext.Empty));

            _negation = new NegateNode(new ConstantNode<int>(3));
            Assert.Equal(-3, _negation.Evaluate(NodeEvaluationContext.Empty));

            _negation = new NegateNode(new ConstantNode<long>(3));
            Assert.Equal(-3L, _negation.Evaluate(NodeEvaluationContext.Empty));

            _negation = new NegateNode(new ConstantNode<float>(3));
            Assert.Equal(-3f, _negation.Evaluate(NodeEvaluationContext.Empty));

            _negation = new NegateNode(new ConstantNode<double>(3));
            Assert.Equal(-3d, _negation.Evaluate(NodeEvaluationContext.Empty));

            _negation = new NegateNode(new ConstantNode<decimal>(3));
            Assert.Equal(-3M, _negation.Evaluate(NodeEvaluationContext.Empty));

            _negation = new NegateNode(new ConstantNode<int>(-3));
            Assert.Equal(3, _negation.Evaluate(NodeEvaluationContext.Empty));
        }
    }
}
