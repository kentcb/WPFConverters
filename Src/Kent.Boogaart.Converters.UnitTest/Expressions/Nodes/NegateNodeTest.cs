using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class NegateNodeTest : UnitTest
	{
		private NegateNode _negation;

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Operator '~' cannot be applied to operand of type 'String'.")]
		public void Evaluate_ShouldThrowIfCannotNegateValue1()
		{
			_negation = new NegateNode(new ConstantNode<string>(""));
			_negation.Evaluate(NodeEvaluationContext.Empty);
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Operator '~' cannot be applied to operand of type 'Boolean'.")]
		public void Evaluate_ShouldThrowIfCannotNegateValue2()
		{
			_negation = new NegateNode(new ConstantNode<bool>(true));
			_negation.Evaluate(NodeEvaluationContext.Empty);
		}

		[Test]
		public void Evaluate_ShouldNegateChildsValue()
		{
			_negation = new NegateNode(new ConstantNode<byte>(3));
			Assert.AreEqual(-3, _negation.Evaluate(NodeEvaluationContext.Empty));

			_negation = new NegateNode(new ConstantNode<short>(3));
			Assert.AreEqual((short) -3, _negation.Evaluate(NodeEvaluationContext.Empty));

			_negation = new NegateNode(new ConstantNode<int>(3));
			Assert.AreEqual(-3, _negation.Evaluate(NodeEvaluationContext.Empty));

			_negation = new NegateNode(new ConstantNode<long>(3));
			Assert.AreEqual(-3L, _negation.Evaluate(NodeEvaluationContext.Empty));

			_negation = new NegateNode(new ConstantNode<float>(3));
			Assert.AreEqual(-3f, _negation.Evaluate(NodeEvaluationContext.Empty));

			_negation = new NegateNode(new ConstantNode<double>(3));
			Assert.AreEqual(-3d, _negation.Evaluate(NodeEvaluationContext.Empty));

			_negation = new NegateNode(new ConstantNode<decimal>(3));
			Assert.AreEqual(-3M, _negation.Evaluate(NodeEvaluationContext.Empty));

			_negation = new NegateNode(new ConstantNode<int>(-3));
			Assert.AreEqual(3, _negation.Evaluate(NodeEvaluationContext.Empty));
		}
	}
}
