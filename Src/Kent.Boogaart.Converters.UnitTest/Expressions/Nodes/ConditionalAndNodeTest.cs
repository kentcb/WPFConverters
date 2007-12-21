using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
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

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("&&", GetPrivateMemberValue<string>(_conditionalAndNode, "OperatorSymbols"));
		}

		[Test]
		public void DetermineResultPreRightEvaluation_ShouldReturnFalseIsLeftIsFalse()
		{
			Assert.IsFalse(InvokePrivateMethod<bool?>(_conditionalAndNode, "DetermineResultPreRightEvaluation", false).Value);
		}

		[Test]
		public void DetermineResultPreRightEvaluation_ShouldReturnNullIsLeftIsTrue()
		{
			Assert.IsNull(InvokePrivateMethod<bool?>(_conditionalAndNode, "DetermineResultPreRightEvaluation", true));
		}

		[Test]
		public void DetermineResultPostRightEvaluation_ShouldReturnRightNodeValue()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_conditionalAndNode, "DetermineResultPostRightEvaluation", false, false));
			Assert.IsFalse(InvokePrivateMethod<bool>(_conditionalAndNode, "DetermineResultPostRightEvaluation", true, false));
			Assert.IsTrue(InvokePrivateMethod<bool>(_conditionalAndNode, "DetermineResultPostRightEvaluation", false, true));
			Assert.IsTrue(InvokePrivateMethod<bool>(_conditionalAndNode, "DetermineResultPostRightEvaluation", true, true));
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
