using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
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
