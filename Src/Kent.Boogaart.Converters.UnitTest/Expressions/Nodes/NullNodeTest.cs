using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class NullNodeTest : UnitTest
	{
		[Test]
		public void Evaluate_ShouldYieldNull()
		{
			Assert.IsNull(NullNode.Instance.Evaluate(NodeEvaluationContext.Empty));
		}
	}
}
