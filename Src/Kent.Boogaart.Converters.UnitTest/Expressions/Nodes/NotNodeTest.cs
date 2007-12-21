using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class NotNodeTest : UnitTest
	{
		private NotNode _notNode;

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Operator '!' cannot be applied to operand of type 'Double'.")]
		public void Evaluate_ShouldThrowIfNotBoolean1()
		{
			_notNode = new NotNode(new ConstantNode<double>(1.2));
			_notNode.Evaluate(NodeEvaluationContext.Empty);
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Operator '!' cannot be applied to operand of type 'Int32'.")]
		public void Evaluate_ShouldThrowIfNotBoolean2()
		{
			_notNode = new NotNode(new ConstantNode<int>(1));
			_notNode.Evaluate(NodeEvaluationContext.Empty);
		}

		[Test]
		public void Evaluate_ShouldNotGivenValue()
		{
			_notNode = new NotNode(new ConstantNode<bool>(false));
			Assert.AreEqual(true, _notNode.Evaluate(NodeEvaluationContext.Empty));

			_notNode = new NotNode(new ConstantNode<bool>(true));
			Assert.AreEqual(false, _notNode.Evaluate(NodeEvaluationContext.Empty));
		}
	}
}
