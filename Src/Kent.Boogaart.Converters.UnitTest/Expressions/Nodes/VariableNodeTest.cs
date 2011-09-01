using Xunit;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class VariableNodeTest : UnitTest
    {
        private VariableNode _variableNode;

        [Fact]
        public void Evaluate_ShouldThrowIfArgumentNotFound()
        {
            _variableNode = new VariableNode(1);
            var ex = Assert.Throws<ParseException>(() => _variableNode.Evaluate(new NodeEvaluationContext(new object[] { "first argument" })));
            Assert.Equal("No argument with index 1 has been supplied.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldReturnCorrespondingArgument()
        {
            NodeEvaluationContext evaluationContext = new NodeEvaluationContext(new object[] { 1, "abc", 2.3f, null, 43d });
            _variableNode = new VariableNode(0);
            Assert.Equal(1, _variableNode.Evaluate(evaluationContext));
            _variableNode = new VariableNode(1);
            Assert.Equal("abc", _variableNode.Evaluate(evaluationContext));
            _variableNode = new VariableNode(2);
            Assert.Equal(2.3f, _variableNode.Evaluate(evaluationContext));
            _variableNode = new VariableNode(3);
            Assert.Null(_variableNode.Evaluate(evaluationContext));
            _variableNode = new VariableNode(4);
            Assert.Equal(43d, _variableNode.Evaluate(evaluationContext));
        }
    }
}
