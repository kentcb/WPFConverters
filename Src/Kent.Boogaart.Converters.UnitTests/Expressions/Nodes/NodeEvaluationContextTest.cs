using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions.Nodes
{
    public sealed class NodeEvaluationContextTest : UnitTest
    {
        private NodeEvaluationContext nodeEvaluationContext;

        protected override void SetUpCore()
        {
             base.SetUpCore();
            object[] args = new object[] { 1, "abc", 3.8d, null };
            this.nodeEvaluationContext = new NodeEvaluationContext(args);
        }

        [Fact]
        public void HasArgument_ShouldDetermineArgumentExistence()
        {
            Assert.True(this.nodeEvaluationContext.HasArgument(0));
            Assert.True(this.nodeEvaluationContext.HasArgument(1));
            Assert.True(this.nodeEvaluationContext.HasArgument(2));
            Assert.True(this.nodeEvaluationContext.HasArgument(3));
            Assert.False(this.nodeEvaluationContext.HasArgument(4));
            Assert.False(this.nodeEvaluationContext.HasArgument(5));
        }

        [Fact]
        public void GetArgument_ShouldYieldArgument()
        {
            Assert.Equal(1, this.nodeEvaluationContext.GetArgument(0));
            Assert.Equal("abc", this.nodeEvaluationContext.GetArgument(1));
            Assert.Equal(3.8d, this.nodeEvaluationContext.GetArgument(2));
            Assert.Null(this.nodeEvaluationContext.GetArgument(3));
        }
    }
}
