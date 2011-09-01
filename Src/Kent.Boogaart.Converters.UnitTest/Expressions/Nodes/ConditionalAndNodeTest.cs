using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class ConditionalAndNodeTest : UnitTest
	{
		private MockNode _leftNode;
		private MockNode _rightNode;
		private ConditionalAndNode _conditionalAndNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_leftNode = new MockNode();
			_rightNode = new MockNode();
			_conditionalAndNode = new ConditionalAndNode(_leftNode, _rightNode);
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("&&", GetPrivateMemberValue<string>(_conditionalAndNode, "OperatorSymbols"));
		}

		[Fact]
		public void DetermineResultPreRightEvaluation_ShouldReturnFalseIsLeftIsFalse()
		{
			Assert.False(InvokePrivateMethod<bool?>(_conditionalAndNode, "DetermineResultPreRightEvaluation", false).Value);
		}

		[Fact]
		public void DetermineResultPreRightEvaluation_ShouldReturnNullIsLeftIsTrue()
		{
			Assert.Null(InvokePrivateMethod<bool?>(_conditionalAndNode, "DetermineResultPreRightEvaluation", true));
		}

		[Fact]
		public void DetermineResultPostRightEvaluation_ShouldReturnRightNodeValue()
		{
			Assert.False(InvokePrivateMethod<bool>(_conditionalAndNode, "DetermineResultPostRightEvaluation", false, false));
			Assert.False(InvokePrivateMethod<bool>(_conditionalAndNode, "DetermineResultPostRightEvaluation", true, false));
			Assert.True(InvokePrivateMethod<bool>(_conditionalAndNode, "DetermineResultPostRightEvaluation", false, true));
			Assert.True(InvokePrivateMethod<bool>(_conditionalAndNode, "DetermineResultPostRightEvaluation", true, true));
		}

		#region Supporting Types

		private sealed class MockNode : Node
		{
			public bool EvaluateTo;
			public bool EvaluateCalled;

			public override object Evaluate(NodeEvaluationContext evaluationContext)
			{
				EvaluateCalled = true;
				return EvaluateTo;
			}
		}

		#endregion
	}
}
