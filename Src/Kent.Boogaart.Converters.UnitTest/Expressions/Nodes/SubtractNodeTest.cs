using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class SubtractNodeTest : UnitTest
	{
		private SubtractNode _subtractNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_subtractNode = new SubtractNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("-", GetPrivateMemberValue<string>(_subtractNode, "OperatorSymbols"));
		}

		[Test]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.IsFalse(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.IsFalse(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.IsTrue(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.IsTrue(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
		}

		[Test]
		public void DoByte_ShouldDoSubtraction()
		{
			Assert.AreEqual(-1, InvokePrivateMethod<int>(_subtractNode, "DoByte", (byte) 1, (byte) 2));
		}

		[Test]
		public void DoInt16_ShouldDoSubtraction()
		{
			Assert.AreEqual(-1, InvokePrivateMethod<int>(_subtractNode, "DoInt16", (short) 1, (short) 2));
		}

		[Test]
		public void DoInt32_ShouldDoSubtraction()
		{
			Assert.AreEqual(-1, InvokePrivateMethod<int>(_subtractNode, "DoInt32", 1, 2));
		}

		[Test]
		public void DoInt64_ShouldDoSubtraction()
		{
			Assert.AreEqual(-1L, InvokePrivateMethod<long>(_subtractNode, "DoInt64", 1L, 2L));
		}

		[Test]
		public void DoSingle_ShouldDoSubtraction()
		{
			Assert.AreEqual(-1f, InvokePrivateMethod<float>(_subtractNode, "DoSingle", 1f, 2f));
		}

		[Test]
		public void DoDouble_ShouldDoSubtraction()
		{
			Assert.AreEqual(-1d, InvokePrivateMethod<double>(_subtractNode, "DoDouble", 1d, 2d));
		}

		[Test]
		public void DoDecimal_ShouldDoSubtraction()
		{
			Assert.AreEqual(-1m, InvokePrivateMethod<decimal>(_subtractNode, "DoDecimal", 1m, 2m));
		}
	}
}
