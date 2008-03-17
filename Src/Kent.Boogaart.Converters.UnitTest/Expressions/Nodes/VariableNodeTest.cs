using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class VariableNodeTest : UnitTest
	{
		private VariableNode _variableNode;

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage="No argument with index 1 has been supplied.")]
		public void Evaluate_ShouldThrowIfArgumentNotFound()
		{
			_variableNode = new VariableNode(1);
			_variableNode.Evaluate(new NodeEvaluationContext(new object[] { "first argument" }));
		}

		[Test]
		public void Evaluate_ShouldReturnCorrespondingArgument()
		{
			NodeEvaluationContext evaluationContext = new NodeEvaluationContext(new object[] { 1, "abc", 2.3f, null, 43d });
			_variableNode = new VariableNode(0);
			Assert.AreEqual(1, _variableNode.Evaluate(evaluationContext));
			_variableNode = new VariableNode(1);
			Assert.AreEqual("abc", _variableNode.Evaluate(evaluationContext));
			_variableNode = new VariableNode(2);
			Assert.AreEqual(2.3f, _variableNode.Evaluate(evaluationContext));
			_variableNode = new VariableNode(3);
			Assert.IsNull(_variableNode.Evaluate(evaluationContext));
			_variableNode = new VariableNode(4);
			Assert.AreEqual(43d, _variableNode.Evaluate(evaluationContext));
		}
	}
}
