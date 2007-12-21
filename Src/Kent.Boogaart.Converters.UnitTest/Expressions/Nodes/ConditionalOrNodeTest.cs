using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class ConditionalOrNodeTest : UnitTest
	{
		private ConditionalOrNode _conditionalOrNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_conditionalOrNode = new ConditionalOrNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("||", GetPrivateMemberValue<string>(_conditionalOrNode, "OperatorSymbols"));
		}

		[Test]
		public void DetermineResultPreRightEvaluation_ShouldReturnTrueIsLeftIsTrue()
		{
			Assert.IsTrue(InvokePrivateMethod<bool?>(_conditionalOrNode, "DetermineResultPreRightEvaluation", true).Value);
		}

		[Test]
		public void DetermineResultPreRightEvaluation_ShouldReturnNullIsLeftIsFalse()
		{
			Assert.IsNull(InvokePrivateMethod<bool?>(_conditionalOrNode, "DetermineResultPreRightEvaluation", false));
		}

		[Test]
		public void DetermineResultPostRightEvaluation_ShouldReturnRightNodeValue()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_conditionalOrNode, "DetermineResultPostRightEvaluation", false, false));
			Assert.IsFalse(InvokePrivateMethod<bool>(_conditionalOrNode, "DetermineResultPostRightEvaluation", true, false));
			Assert.IsTrue(InvokePrivateMethod<bool>(_conditionalOrNode, "DetermineResultPostRightEvaluation", false, true));
			Assert.IsTrue(InvokePrivateMethod<bool>(_conditionalOrNode, "DetermineResultPostRightEvaluation", true, true));
		}
	}
}
