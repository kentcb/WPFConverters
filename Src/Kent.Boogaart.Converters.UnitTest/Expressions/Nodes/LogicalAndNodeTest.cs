using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class LogicalAndNodeTest : UnitTest
	{
		private LogicalAndNode _logicalAndNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_logicalAndNode = new LogicalAndNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("&", GetPrivateMemberValue<string>(_logicalAndNode, "OperatorSymbols"));
		}

		[Test]
		public void IsSupported_ShouldYieldTrueOnlyIfBothBooleanOrNumericalTypes()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.Boolean, NodeValueType.Boolean));
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.Int16, NodeValueType.Int32));

			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.Boolean, NodeValueType.Int16));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.String, NodeValueType.Int16));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.String, NodeValueType.String));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.Single, NodeValueType.Single));
		}

		[Test]
		public void DoBoolean_ShouldDoLogic()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_logicalAndNode, "DoBoolean", true, true));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalAndNode, "DoBoolean", false, true));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalAndNode, "DoBoolean", true, false));
			Assert.IsFalse(InvokePrivateMethod<bool>(_logicalAndNode, "DoBoolean", false, false));
		}

		[Test]
		public void DoByte_ShouldDoLogic()
		{
			Assert.AreEqual(1, InvokePrivateMethod<int>(_logicalAndNode, "DoByte", (byte) 5, (byte) 3));
		}

		[Test]
		public void DoInt16_ShouldDoLogic()
		{
			Assert.AreEqual(1, InvokePrivateMethod<int>(_logicalAndNode, "DoInt16", (short) 5, (short) 3));
		}

		[Test]
		public void DoInt32_ShouldDoLogic()
		{
			Assert.AreEqual(1, InvokePrivateMethod<int>(_logicalAndNode, "DoInt32", 5, 3));
		}

		[Test]
		public void DoInt64_ShouldDoLogic()
		{
			Assert.AreEqual(1L, InvokePrivateMethod<long>(_logicalAndNode, "DoInt64", 5L, 3L));
		}
	}
}
