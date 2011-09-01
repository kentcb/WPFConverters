using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class ConstantNodeTest : UnitTest
	{
		private ConstantNode<int> _intConstantNode;
		private ConstantNode<float> _floatConstantNode;

		[Fact]
		public void Value_ShouldYieldAssignedValue()
		{
			CreateNodes(3, 3.8f);
			Assert.Equal(3, _intConstantNode.Value);
			Assert.Equal(3.8f, _floatConstantNode.Value);
		}

		[Fact]
		public void Evaluate_ShouldReturnAssignedValue()
		{
			CreateNodes(3, 3.8f);
			Assert.Equal(3, _intConstantNode.Evaluate(NodeEvaluationContext.Empty));
			Assert.Equal(3.8f, _floatConstantNode.Evaluate(NodeEvaluationContext.Empty));
		}

		#region Helper methods

		private void CreateNodes(int intConstant, float floatConstant)
		{
			_intConstantNode = new ConstantNode<int>(intConstant);
			_floatConstantNode = new ConstantNode<float>(floatConstant);
		}

		#endregion
	}
}
