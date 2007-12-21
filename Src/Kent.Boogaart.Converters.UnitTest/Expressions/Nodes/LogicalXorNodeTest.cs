using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class LogicalXorNodeTest : UnitTest
	{
		private LogicalXorNode _logicalXorNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_logicalXorNode = new LogicalXorNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("^", GetPrivateMemberValue<string>(_logicalXorNode, "OperatorSymbols"));
		}

		[Test]
		public void IsSupported_ShouldYieldTrueOnlyIfBothBooleanOrNumericalTypes()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.Boolean, NodeValueType.Boolean));
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.Int16, NodeValueType.Int32));

			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.Boolean, NodeValueType.Int16));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.String, NodeValueType.Int16));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.String, NodeValueType.String));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.Single, NodeValueType.Single));
		}

		[Test]
		public void DoBoolean_ShouldDoLogic()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalXorNode, "DoBoolean", true, true));
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalXorNode, "DoBoolean", false, true));
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalXorNode, "DoBoolean", true, false));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalXorNode, "DoBoolean", false, false));
		}

		[Test]
		public void DoByte_ShouldDoLogic()
		{
			Assert.AreEqual(6, InvokePrivateMethod<int>(_logicalXorNode, "DoByte", (byte) 5, (byte) 3));
		}

		[Test]
		public void DoInt16_ShouldDoLogic()
		{
			Assert.AreEqual(6, InvokePrivateMethod<int>(_logicalXorNode, "DoInt16", (short) 5, (short) 3));
		}

		[Test]
		public void DoInt32_ShouldDoLogic()
		{
			Assert.AreEqual(6, InvokePrivateMethod<int>(_logicalXorNode, "DoInt32", 5, 3));
		}

		[Test]
		public void DoInt64_ShouldDoLogic()
		{
			Assert.AreEqual(6L, InvokePrivateMethod<long>(_logicalXorNode, "DoInt64", 5L, 3L));
		}
	}
}
