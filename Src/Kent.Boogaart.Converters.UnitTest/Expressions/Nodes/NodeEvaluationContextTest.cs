using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class NodeEvaluationContextTest : UnitTest
	{
		private NodeEvaluationContext _nodeEvaluationContext;

		protected override void  SetUpCore()
		{
			 base.SetUpCore();
			object[] args = new object[] { 1, "abc", 3.8d, null };
			_nodeEvaluationContext = new NodeEvaluationContext(args);
		}

		[Fact]
		public void HasArgument_ShouldDetermineArgumentExistence()
		{
			Assert.True(_nodeEvaluationContext.HasArgument(0));
			Assert.True(_nodeEvaluationContext.HasArgument(1));
			Assert.True(_nodeEvaluationContext.HasArgument(2));
			Assert.True(_nodeEvaluationContext.HasArgument(3));
			Assert.False(_nodeEvaluationContext.HasArgument(4));
			Assert.False(_nodeEvaluationContext.HasArgument(5));
		}

		[Fact]
		public void GetArgument_ShouldYieldArgument()
		{
			Assert.Equal(1, _nodeEvaluationContext.GetArgument(0));
			Assert.Equal("abc", _nodeEvaluationContext.GetArgument(1));
			Assert.Equal(3.8d, _nodeEvaluationContext.GetArgument(2));
			Assert.Null(_nodeEvaluationContext.GetArgument(3));
		}
	}
}
