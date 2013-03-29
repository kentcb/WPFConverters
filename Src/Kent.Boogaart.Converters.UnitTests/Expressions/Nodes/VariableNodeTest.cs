using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions.Nodes
{
    public sealed class VariableNodeTest : UnitTest
    {
        private VariableNode variableNode;

        [Fact]
        public void Evaluate_ShouldThrowIfArgumentNotFound()
        {
            this.variableNode = new VariableNode(1);
            var ex = Assert.Throws<ParseException>(() => this.variableNode.Evaluate(new NodeEvaluationContext(new object[] { "first argument" })));
            Assert.Equal("No argument with index 1 has been supplied.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldReturnCorrespondingArgument()
        {
            NodeEvaluationContext evaluationContext = new NodeEvaluationContext(new object[] { 1, "abc", 2.3f, null, 43d });
            this.variableNode = new VariableNode(0);
            Assert.Equal(1, this.variableNode.Evaluate(evaluationContext));
            this.variableNode = new VariableNode(1);
            Assert.Equal("abc", this.variableNode.Evaluate(evaluationContext));
            this.variableNode = new VariableNode(2);
            Assert.Equal(2.3f, this.variableNode.Evaluate(evaluationContext));
            this.variableNode = new VariableNode(3);
            Assert.Null(this.variableNode.Evaluate(evaluationContext));
            this.variableNode = new VariableNode(4);
            Assert.Equal(43d, this.variableNode.Evaluate(evaluationContext));
        }
    }
}
