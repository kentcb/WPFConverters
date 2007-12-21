using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class EqualityNodeTest : UnitTest
	{
		private EqualityNode _equalityNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_equalityNode = new EqualityNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("==", GetPrivateMemberValue<string>(_equalityNode, "OperatorSymbols"));
		}

		[Test]
		public void IsSupported_ShouldYieldTrueIfBothStringsOrBothNumeric()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.String, NodeValueType.String));
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.String, NodeValueType.Null));
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Null, NodeValueType.String));
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Null, NodeValueType.Null));
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
			Assert.IsFalse(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.IsFalse(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Unknown, NodeValueType.Int32));
		}

		[Test]
		public void DoString_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "DoString", "abc", "abc"));
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "DoString", null, null));
			Assert.IsFalse(InvokePrivateMethod<bool>(_equalityNode, "DoString", "abc", null));
			Assert.IsFalse(InvokePrivateMethod<bool>(_equalityNode, "DoString", "abc", "abcd"));
		}

		[Test]
		public void DoBoolean_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "DoBoolean", true, true));
			Assert.IsFalse(InvokePrivateMethod<bool>(_equalityNode, "DoBoolean", true, false));
		}

		[Test]
		public void DoByte_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "DoByte", (byte) 1, (byte) 1));
			Assert.IsFalse(InvokePrivateMethod<bool>(_equalityNode, "DoByte", (byte) 1, (byte) 2));
		}

		[Test]
		public void DoInt16_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "DoInt16", (short) 1, (short) 1));
			Assert.IsFalse(InvokePrivateMethod<bool>(_equalityNode, "DoInt16", (short) 1, (short) 2));
		}

		[Test]
		public void DoInt32_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "DoInt32", 1, 1));
			Assert.IsFalse(InvokePrivateMethod<bool>(_equalityNode, "DoInt32", 1, 2));
		}

		[Test]
		public void DoInt64_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "DoInt64", 1L, 1L));
			Assert.IsFalse(InvokePrivateMethod<bool>(_equalityNode, "DoInt64", 1L, 2L));
		}

		[Test]
		public void DoSingle_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "DoSingle", 1f, 1f));
			Assert.IsFalse(InvokePrivateMethod<bool>(_equalityNode, "DoSingle", 1f, 2f));
		}

		[Test]
		public void DoDouble_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "DoDouble", 1d, 1d));
			Assert.IsFalse(InvokePrivateMethod<bool>(_equalityNode, "DoDouble", 1d, 2d));
		}

		[Test]
		public void DoDecimal_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_equalityNode, "DoDecimal", 1m, 1m));
			Assert.IsFalse(InvokePrivateMethod<bool>(_equalityNode, "DoDecimal", 1m, 2m));
		}
	}
}
