using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class ComplementNodeTest : UnitTest
	{
		private ComplementNode _complementNode;

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage="Operator '~' cannot be applied to operand of type 'Boolean'.")]
		public void Evaluate_ShouldThrowIfNotIntegral1()
		{
			_complementNode = new ComplementNode(new ConstantNode<bool>(true));
			_complementNode.Evaluate(NodeEvaluationContext.Empty);
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Operator '~' cannot be applied to operand of type 'Double'.")]
		public void Evaluate_ShouldThrowIfNotIntegral2()
		{
			_complementNode = new ComplementNode(new ConstantNode<double>(1.3));
			_complementNode.Evaluate(NodeEvaluationContext.Empty);
		}

		[Test]
		public void Evaluate_ShouldComplementGivenValue()
		{
			_complementNode = new ComplementNode(new ConstantNode<byte>(1));
			Assert.AreEqual(~1, _complementNode.Evaluate(NodeEvaluationContext.Empty));

			_complementNode = new ComplementNode(new ConstantNode<short>(1));
			Assert.AreEqual(~1, _complementNode.Evaluate(NodeEvaluationContext.Empty));

			_complementNode = new ComplementNode(new ConstantNode<int>(1));
			Assert.AreEqual(~1, _complementNode.Evaluate(NodeEvaluationContext.Empty));

			_complementNode = new ComplementNode(new ConstantNode<long>(1));
			Assert.AreEqual(~1, _complementNode.Evaluate(NodeEvaluationContext.Empty));
		}
	}
}
