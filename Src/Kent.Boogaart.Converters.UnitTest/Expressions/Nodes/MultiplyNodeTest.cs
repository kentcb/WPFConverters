using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class MultiplyNodeTest : UnitTest
	{
		private MultiplyNode _multiplyNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_multiplyNode = new MultiplyNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("*", GetPrivateMemberValue<string>(_multiplyNode, "OperatorSymbols"));
		}

		[Test]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.IsFalse(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.IsFalse(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.IsTrue(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.IsTrue(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
		}

		[Test]
		public void DoByte_ShouldDoMultiplication()
		{
			Assert.AreEqual(2, InvokePrivateMethod<int>(_multiplyNode, "DoByte", (byte) 1, (byte) 2));
		}

		[Test]
		public void DoInt16_ShouldDoMultiplication()
		{
			Assert.AreEqual(2, InvokePrivateMethod<int>(_multiplyNode, "DoInt16", (short) 1, (short) 2));
		}

		[Test]
		public void DoInt32_ShouldDoMultiplication()
		{
			Assert.AreEqual(2, InvokePrivateMethod<int>(_multiplyNode, "DoInt32", 1, 2));
		}

		[Test]
		public void DoInt64_ShouldDoMultiplication()
		{
			Assert.AreEqual(2L, InvokePrivateMethod<long>(_multiplyNode, "DoInt64", 1L, 2L));
		}

		[Test]
		public void DoSingle_ShouldDoMultiplication()
		{
			Assert.AreEqual(2f, InvokePrivateMethod<float>(_multiplyNode, "DoSingle", 1f, 2f));
		}

		[Test]
		public void DoDouble_ShouldDoMultiplication()
		{
			Assert.AreEqual(2d, InvokePrivateMethod<double>(_multiplyNode, "DoDouble", 1d, 2d));
		}

		[Test]
		public void DoDecimal_ShouldDoMultiplication()
		{
			Assert.AreEqual(2m, InvokePrivateMethod<decimal>(_multiplyNode, "DoDecimal", 1m, 2m));
		}
	}
}
