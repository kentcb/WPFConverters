using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class UnaryNodeTest : UnitTest
	{
		private MockUnaryNode _unaryNode;
		private Node _child;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_child = new ConstantNode<int>(0);
			_unaryNode = new MockUnaryNode(_child);
		}

		[Test]
		public void Node_ShouldYieldGivenNode()
		{
			Assert.AreSame(_child, _unaryNode.Node);
		}

		#region Supporting Types

		//cannot mock because UnaryNode is internal
		private sealed class MockUnaryNode : UnaryNode
		{
			public MockUnaryNode(Node node)
				: base(node)
			{
			}

			public override object Evaluate(NodeEvaluationContext evaluationContext)
			{
				return null;
			}
		}

		#endregion
	}
}
