using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class InequalityNodeTest : UnitTest
	{
		private InequalityNode _inequalityNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_inequalityNode = new InequalityNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("!=", GetPrivateMemberValue<string>(_inequalityNode, "OperatorSymbols"));
		}

		[Test]
		public void IsSupported_ShouldYieldTrueIfBothStringsOrBothNumeric()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.String, NodeValueType.String));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.String, NodeValueType.Null));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Null, NodeValueType.String));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Null, NodeValueType.Null));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
			Assert.IsFalse(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.IsFalse(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Unknown, NodeValueType.Int32));
		}

		[Test]
		public void DoString_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_inequalityNode, "DoString", "abc", "abc"));
			Assert.IsFalse(InvokePrivateMethod<bool>(_inequalityNode, "DoString", null, null));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "DoString", "abc", null));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "DoString", "abc", "abcd"));
		}

		[Test]
		public void DoBoolean_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_inequalityNode, "DoBoolean", true, true));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "DoBoolean", true, false));
		}

		[Test]
		public void DoByte_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_inequalityNode, "DoByte", (byte) 1, (byte) 1));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "DoByte", (byte) 1, (byte) 2));
		}

		[Test]
		public void DoInt16_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_inequalityNode, "DoInt16", (short) 1, (short) 1));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "DoInt16", (short) 1, (short) 2));
		}

		[Test]
		public void DoInt32_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_inequalityNode, "DoInt32", 1, 1));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "DoInt32", 1, 2));
		}

		[Test]
		public void DoInt64_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_inequalityNode, "DoInt64", 1L, 1L));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "DoInt64", 1L, 2L));
		}

		[Test]
		public void DoSingle_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_inequalityNode, "DoSingle", 1f, 1f));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "DoSingle", 1f, 2f));
		}

		[Test]
		public void DoDouble_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_inequalityNode, "DoDouble", 1d, 1d));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "DoDouble", 1d, 2d));
		}

		[Test]
		public void DoDecimal_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_inequalityNode, "DoDecimal", 1m, 1m));
			Assert.IsTrue(InvokePrivateMethod<bool>(_inequalityNode, "DoDecimal", 1m, 2m));
		}
	}
}
