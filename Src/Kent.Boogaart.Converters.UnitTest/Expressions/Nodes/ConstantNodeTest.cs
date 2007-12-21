using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class ConstantNodeTest : UnitTest
	{
		private ConstantNode<int> _intConstantNode;
		private ConstantNode<float> _floatConstantNode;

		[Test]
		public void Value_ShouldYieldAssignedValue()
		{
			CreateNodes(3, 3.8f);
			Assert.AreEqual(3, _intConstantNode.Value);
			Assert.AreEqual(3.8f, _floatConstantNode.Value);
		}

		[Test]
		public void Evaluate_ShouldReturnAssignedValue()
		{
			CreateNodes(3, 3.8f);
			Assert.AreEqual(3, _intConstantNode.Evaluate(NodeEvaluationContext.Empty));
			Assert.AreEqual(3.8f, _floatConstantNode.Evaluate(NodeEvaluationContext.Empty));
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
