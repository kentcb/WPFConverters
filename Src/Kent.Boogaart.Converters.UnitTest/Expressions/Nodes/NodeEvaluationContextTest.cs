using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class NodeEvaluationContextTest : UnitTest
	{
		private NodeEvaluationContext _nodeEvaluationContext;

		protected override void  SetUpCore()
		{
			 base.SetUpCore();
			object[] args = new object[] { 1, "abc", 3.8d, null };
			_nodeEvaluationContext = new NodeEvaluationContext(args);
		}

		[Test]
		public void HasArgument_ShouldDetermineArgumentExistence()
		{
			Assert.IsTrue(_nodeEvaluationContext.HasArgument(0));
			Assert.IsTrue(_nodeEvaluationContext.HasArgument(1));
			Assert.IsTrue(_nodeEvaluationContext.HasArgument(2));
			Assert.IsTrue(_nodeEvaluationContext.HasArgument(3));
			Assert.IsFalse(_nodeEvaluationContext.HasArgument(4));
			Assert.IsFalse(_nodeEvaluationContext.HasArgument(5));
		}

		[Test]
		public void GetArgument_ShouldYieldArgument()
		{
			Assert.AreEqual(1, _nodeEvaluationContext.GetArgument(0));
			Assert.AreEqual("abc", _nodeEvaluationContext.GetArgument(1));
			Assert.AreEqual(3.8d, _nodeEvaluationContext.GetArgument(2));
			Assert.IsNull(_nodeEvaluationContext.GetArgument(3));
		}
	}
}
