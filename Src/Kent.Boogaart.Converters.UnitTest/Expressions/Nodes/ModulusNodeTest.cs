using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class ModulusNodeTest : UnitTest
	{
		private ModulusNode _modulusNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_modulusNode = new ModulusNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("%", GetPrivateMemberValue<string>(_modulusNode, "OperatorSymbols"));
		}

		[Test]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.IsFalse(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.IsFalse(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.IsTrue(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.IsTrue(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
		}

		[Test]
		public void DoByte_ShouldDoModulus()
		{
			Assert.AreEqual(1, InvokePrivateMethod<int>(_modulusNode, "DoByte", (byte) 5, (byte) 2));
		}

		[Test]
		public void DoInt16_ShouldDoModulus()
		{
			Assert.AreEqual(1, InvokePrivateMethod<int>(_modulusNode, "DoInt16", (short) 5, (short) 2));
		}

		[Test]
		public void DoInt32_ShouldDoModulus()
		{
			Assert.AreEqual(1, InvokePrivateMethod<int>(_modulusNode, "DoInt32", 5, 2));
		}

		[Test]
		public void DoInt64_ShouldDoModulus()
		{
			Assert.AreEqual(1L, InvokePrivateMethod<long>(_modulusNode, "DoInt64", 5L, 2L));
		}

		[Test]
		public void DoSingle_ShouldDoModulus()
		{
			Assert.AreEqual(1f, InvokePrivateMethod<float>(_modulusNode, "DoSingle", 5f, 2f));
		}

		[Test]
		public void DoDouble_ShouldDoModulus()
		{
			Assert.AreEqual(1d, InvokePrivateMethod<double>(_modulusNode, "DoDouble", 5d, 2d));
		}

		[Test]
		public void DoDecimal_ShouldDoModulus()
		{
			Assert.AreEqual(1m, InvokePrivateMethod<decimal>(_modulusNode, "DoDecimal", 5m, 2m));
		}
	}
}
