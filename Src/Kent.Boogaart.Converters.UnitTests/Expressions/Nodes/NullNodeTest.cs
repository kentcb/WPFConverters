using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions.Nodes
{
    public sealed class NullNodeTest : UnitTest
    {
        [Fact]
        public void Evaluate_ShouldYieldNull()
        {
            Assert.Null(NullNode.Instance.Evaluate(NodeEvaluationContext.Empty));
        }
    }
}
