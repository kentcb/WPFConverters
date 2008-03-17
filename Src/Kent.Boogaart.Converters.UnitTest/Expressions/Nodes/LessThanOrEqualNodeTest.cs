using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class LessThanOrEqualNodeTest : UnitTest
	{
		private LessThanOrEqualNode _lessThanOrEqualNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_lessThanOrEqualNode = new LessThanOrEqualNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("<=", GetPrivateMemberValue<string>(_lessThanOrEqualNode, "OperatorSymbols"));
		}

		[Test]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
		}

		[Test]
		public void DoByte_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoByte", (byte) 1, (byte) 2));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoByte", (byte) 2, (byte) 2));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoByte", (byte) 3, (byte) 2));
		}

		[Test]
		public void DoInt16_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt16", (short) 1, (short) 2));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt16", (short) 2, (short) 2));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt16", (short) 3, (short) 2));
		}

		[Test]
		public void DoInt32_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt32", 1, 2));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt32", 2, 2));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt32", 3, 2));
		}

		[Test]
		public void DoInt64_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt64", 1L, 2L));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt64", 2L, 2L));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt64", 3L, 2L));
		}

		[Test]
		public void DoSingle_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoSingle", 1f, 2f));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoSingle", 2f, 2f));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoSingle", 3f, 2f));
		}

		[Test]
		public void DoDouble_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoDouble", 1d, 2d));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoDouble", 2d, 2d));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoDouble", 3d, 2d));
		}

		[Test]
		public void DoDecimal_ShouldDoComparison()
		{
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoDecimal", 1m, 2m));
			Assert.IsTrue(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoDecimal", 2m, 2m));
			Assert.IsFalse(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoDecimal", 3m, 2m));
		}
	}
}
