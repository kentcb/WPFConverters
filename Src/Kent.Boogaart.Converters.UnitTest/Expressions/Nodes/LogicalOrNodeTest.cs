using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class LogicalOrNodeTest : UnitTest
	{
		private LogicalOrNode _logicalOrNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_logicalOrNode = new LogicalOrNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("|", GetPrivateMemberValue<string>(_logicalOrNode, "OperatorSymbols"));
		}

		[Test]
		public void IsSupported_ShouldYieldTrueOnlyIfBothBooleanOrNumericalTypes()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalOrNode, "IsSupported", NodeValueType.Boolean, NodeValueType.Boolean));
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalOrNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalOrNode, "IsSupported", NodeValueType.Int16, NodeValueType.Int32));

			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalOrNode, "IsSupported", NodeValueType.Boolean, NodeValueType.Int16));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalOrNode, "IsSupported", NodeValueType.String, NodeValueType.Int16));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalOrNode, "IsSupported", NodeValueType.String, NodeValueType.String));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalOrNode, "IsSupported", NodeValueType.Single, NodeValueType.Single));
		}

		[Test]
		public void DoBoolean_ShouldDoLogic()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalOrNode, "DoBoolean", true, true));
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalOrNode, "DoBoolean", false, true));
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalOrNode, "DoBoolean", true, false));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalOrNode, "DoBoolean", false, false));
		}

		[Test]
		public void DoByte_ShouldDoLogic()
		{
			Assert.AreEqual(7, InvokePrivateMethod<int>(_logicalOrNode, "DoByte", (byte) 5, (byte) 3));
		}

		[Test]
		public void DoInt16_ShouldDoLogic()
		{
			Assert.AreEqual(7, InvokePrivateMethod<int>(_logicalOrNode, "DoInt16", (short) 5, (short) 3));
		}

		[Test]
		public void DoInt32_ShouldDoLogic()
		{
			Assert.AreEqual(7, InvokePrivateMethod<int>(_logicalOrNode, "DoInt32", 5, 3));
		}

		[Test]
		public void DoInt64_ShouldDoLogic()
		{
			Assert.AreEqual(7L, InvokePrivateMethod<long>(_logicalOrNode, "DoInt64", 5L, 3L));
		}
	}
}
