using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class DivideNodeTest : UnitTest
	{
		private DivideNode _divideNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_divideNode = new DivideNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("/", GetPrivateMemberValue<string>(_divideNode, "OperatorSymbols"));
		}

		[Test]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.IsFalse(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.IsFalse(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.IsFalse(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.IsTrue(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.IsTrue(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.IsTrue(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
		}

		[Test]
		public void DoByte_ShouldDoSubtraction()
		{
			Assert.AreEqual(2, InvokePrivateMethod<int>(_divideNode, "DoByte", (byte) 4, (byte) 2));
		}

		[Test]
		public void DoInt16_ShouldDoSubtraction()
		{
			Assert.AreEqual(2, InvokePrivateMethod<int>(_divideNode, "DoInt16", (short) 4, (short) 2));
		}

		[Test]
		public void DoInt32_ShouldDoSubtraction()
		{
			Assert.AreEqual(2, InvokePrivateMethod<int>(_divideNode, "DoInt32", 4, 2));
		}

		[Test]
		public void DoInt64_ShouldDoSubtraction()
		{
			Assert.AreEqual(2L, InvokePrivateMethod<long>(_divideNode, "DoInt64", 4L, 2L));
		}

		[Test]
		public void DoSingle_ShouldDoSubtraction()
		{
			Assert.AreEqual(2f, InvokePrivateMethod<float>(_divideNode, "DoSingle", 4f, 2f));
		}

		[Test]
		public void DoDouble_ShouldDoSubtraction()
		{
			Assert.AreEqual(2d, InvokePrivateMethod<double>(_divideNode, "DoDouble", 4d, 2d));
		}

		[Test]
		public void DoDecimal_ShouldDoSubtraction()
		{
			Assert.AreEqual(2m, InvokePrivateMethod<decimal>(_divideNode, "DoDecimal", 4m, 2m));
		}
	}
}
