using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class ConditionalOrNodeTest : UnitTest
	{
		private ConditionalOrNode _conditionalOrNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_conditionalOrNode = new ConditionalOrNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("||", GetPrivateMemberValue<string>(_conditionalOrNode, "OperatorSymbols"));
		}

		[Fact]
		public void DetermineResultPreRightEvaluation_ShouldReturnTrueIsLeftIsTrue()
		{
			Assert.True(InvokePrivateMethod<bool?>(_conditionalOrNode, "DetermineResultPreRightEvaluation", true).Value);
		}

		[Fact]
		public void DetermineResultPreRightEvaluation_ShouldReturnNullIsLeftIsFalse()
		{
			Assert.Null(InvokePrivateMethod<bool?>(_conditionalOrNode, "DetermineResultPreRightEvaluation", false));
		}

		[Fact]
		public void DetermineResultPostRightEvaluation_ShouldReturnRightNodeValue()
		{
			Assert.False(InvokePrivateMethod<bool>(_conditionalOrNode, "DetermineResultPostRightEvaluation", false, false));
			Assert.False(InvokePrivateMethod<bool>(_conditionalOrNode, "DetermineResultPostRightEvaluation", true, false));
			Assert.True(InvokePrivateMethod<bool>(_conditionalOrNode, "DetermineResultPostRightEvaluation", false, true));
			Assert.True(InvokePrivateMethod<bool>(_conditionalOrNode, "DetermineResultPostRightEvaluation", true, true));
		}
	}
}
