using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class AddNodeTest : UnitTest
	{
		private AddNode _addNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_addNode = new AddNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("+", GetPrivateMemberValue<string>(_addNode, "OperatorSymbols"));
		}

		[Test]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.IsFalse(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.IsFalse(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.IsTrue(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.IsTrue(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
			Assert.IsTrue(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.String, NodeValueType.String));
		}

		[Test]
		public void DoString_ShouldDoConcatenation()
		{
			Assert.AreEqual("onetwo", InvokePrivateMethod<string>(_addNode, "DoString", "one", "two"));
		}

		[Test]
		public void DoByte_ShouldDoAddition()
		{
			Assert.AreEqual(3, InvokePrivateMethod<int>(_addNode, "DoByte", (byte) 1, (byte) 2));
		}

		[Test]
		public void DoInt16_ShouldDoAddition()
		{
			Assert.AreEqual(3, InvokePrivateMethod<int>(_addNode, "DoInt16", (short) 1, (short) 2));
		}

		[Test]
		public void DoInt32_ShouldDoAddition()
		{
			Assert.AreEqual(3, InvokePrivateMethod<int>(_addNode, "DoInt32", 1, 2));
		}

		[Test]
		public void DoInt64_ShouldDoAddition()
		{
			Assert.AreEqual(3L, InvokePrivateMethod<long>(_addNode, "DoInt64", 1L, 2L));
		}

		[Test]
		public void DoSingle_ShouldDoAddition()
		{
			Assert.AreEqual(3f, InvokePrivateMethod<float>(_addNode, "DoSingle", 1f, 2f));
		}

		[Test]
		public void DoDouble_ShouldDoAddition()
		{
			Assert.AreEqual(3d, InvokePrivateMethod<double>(_addNode, "DoDouble", 1d, 2d));
		}

		[Test]
		public void DoDecimal_ShouldDoAddition()
		{
			Assert.AreEqual(3m, InvokePrivateMethod<decimal>(_addNode, "DoDecimal", 1m, 2m));
		}
	}
}
