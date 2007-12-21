using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class GreatThanNodeTest : UnitTest
	{
		private GreaterThanNode _greaterThanNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_greaterThanNode = new GreaterThanNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual(">", GetPrivateMemberValue<string>(_greaterThanNode, "OperatorSymbols"));
		}

		[Test]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.IsTrue(InvokePrivateMethod<bool>(_greaterThanNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_greaterThanNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_greaterThanNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.IsTrue(InvokePrivateMethod<bool>(_greaterThanNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
		}

		[Test]
		public void DoByte_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoByte", (byte) 1, (byte) 2));
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoByte", (byte) 2, (byte) 2));
			Assert.IsTrue(InvokePrivateMethod<bool>(_greaterThanNode, "DoByte", (byte) 3, (byte) 2));
		}

		[Test]
		public void DoInt16_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoInt16", (short) 1, (short) 2));
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoInt16", (short) 2, (short) 2));
			Assert.IsTrue(InvokePrivateMethod<bool>(_greaterThanNode, "DoInt16", (short) 3, (short) 2));
		}

		[Test]
		public void DoInt32_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoInt32", 1, 2));
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoInt32", 2, 2));
			Assert.IsTrue(InvokePrivateMethod<bool>(_greaterThanNode, "DoInt32", 3, 2));
		}

		[Test]
		public void DoInt64_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoInt64", 1L, 2L));
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoInt64", 2L, 2L));
			Assert.IsTrue(InvokePrivateMethod<bool>(_greaterThanNode, "DoInt64", 3L, 2L));
		}

		[Test]
		public void DoSingle_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoSingle", 1f, 2f));
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoSingle", 2f, 2f));
			Assert.IsTrue(InvokePrivateMethod<bool>(_greaterThanNode, "DoSingle", 3f, 2f));
		}

		[Test]
		public void DoDouble_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoDouble", 1d, 2d));
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoDouble", 2d, 2d));
			Assert.IsTrue(InvokePrivateMethod<bool>(_greaterThanNode, "DoDouble", 3d, 2d));
		}

		[Test]
		public void DoDecimal_ShouldDoComparison()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoDecimal", 1m, 2m));
			Assert.IsFalse(InvokePrivateMethod<bool>(_greaterThanNode, "DoDecimal", 2m, 2m));
			Assert.IsTrue(InvokePrivateMethod<bool>(_greaterThanNode, "DoDecimal", 3m, 2m));
		}
	}
}
