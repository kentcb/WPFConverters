using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class LessThanNodeTest : UnitTest
	{
		private LessThanNode _lessThanNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_lessThanNode = new LessThanNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("<", GetPrivateMemberValue<string>(_lessThanNode, "OperatorSymbols"));
		}

		[Test]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
		}

		[Test]
		public void DoByte_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanNode, "DoByte", (byte) 1, (byte) 2));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoByte", (byte) 2, (byte) 2));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoByte", (byte) 3, (byte) 2));
		}

		[Test]
		public void DoInt16_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanNode, "DoInt16", (short) 1, (short) 2));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoInt16", (short) 2, (short) 2));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoInt16", (short) 3, (short) 2));
		}

		[Test]
		public void DoInt32_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanNode, "DoInt32", 1, 2));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoInt32", 2, 2));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoInt32", 3, 2));
		}

		[Test]
		public void DoInt64_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanNode, "DoInt64", 1L, 2L));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoInt64", 2L, 2L));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoInt64", 3L, 2L));
		}

		[Test]
		public void DoSingle_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanNode, "DoSingle", 1f, 2f));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoSingle", 2f, 2f));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoSingle", 3f, 2f));
		}

		[Test]
		public void DoDouble_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanNode, "DoDouble", 1d, 2d));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoDouble", 2d, 2d));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoDouble", 3d, 2d));
		}

		[Test]
		public void DoDecimal_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanNode, "DoDecimal", 1m, 2m));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoDecimal", 2m, 2m));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanNode, "DoDecimal", 3m, 2m));
		}
	}
}
