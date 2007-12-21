using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class BinaryNodeTest : UnitTest
	{
		private MockBinaryNode _binaryNode;
		private Node _leftNode;
		private Node _rightNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_leftNode = new ConstantNode<int>(0);
			_rightNode = new ConstantNode<int>(0);
			_binaryNode = new MockBinaryNode(_leftNode, _rightNode);
		}

		[Test]
		public void Left_ShouldYieldGivenNode()
		{
			Assert.AreSame(_leftNode, _binaryNode.LeftNode);
		}

		[Test]
		public void Right_ShouldYieldGivenNode()
		{
			Assert.AreSame(_rightNode, _binaryNode.RightNode);
		}

		#region Supporting Types

		//cannot mock because BinaryNode is internal
		private sealed class MockBinaryNode : BinaryNode
		{
			protected override string OperatorSymbols
			{
				get
				{
					return "op";
				}
			}

			public MockBinaryNode(Node leftNode, Node rightNode)
				: base(leftNode, rightNode)
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
