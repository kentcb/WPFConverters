using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class MultiplyNodeTest : UnitTest
	{
		private MultiplyNode _multiplyNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_multiplyNode = new MultiplyNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("*", GetPrivateMemberValue<string>(_multiplyNode, "OperatorSymbols"));
		}

		[Fact]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.False(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.False(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.False(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.True(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.True(InvokePrivateMethod<bool>(_multiplyNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
		}

		[Fact]
		public void DoByte_ShouldDoMultiplication()
		{
			Assert.Equal(2, InvokePrivateMethod<int>(_multiplyNode, "DoByte", (byte) 1, (byte) 2));
		}

		[Fact]
		public void DoInt16_ShouldDoMultiplication()
		{
			Assert.Equal(2, InvokePrivateMethod<int>(_multiplyNode, "DoInt16", (short) 1, (short) 2));
		}

		[Fact]
		public void DoInt32_ShouldDoMultiplication()
		{
			Assert.Equal(2, InvokePrivateMethod<int>(_multiplyNode, "DoInt32", 1, 2));
		}

		[Fact]
		public void DoInt64_ShouldDoMultiplication()
		{
			Assert.Equal(2L, InvokePrivateMethod<long>(_multiplyNode, "DoInt64", 1L, 2L));
		}

		[Fact]
		public void DoSingle_ShouldDoMultiplication()
		{
			Assert.Equal(2f, InvokePrivateMethod<float>(_multiplyNode, "DoSingle", 1f, 2f));
		}

		[Fact]
		public void DoDouble_ShouldDoMultiplication()
		{
			Assert.Equal(2d, InvokePrivateMethod<double>(_multiplyNode, "DoDouble", 1d, 2d));
		}

		[Fact]
		public void DoDecimal_ShouldDoMultiplication()
		{
			Assert.Equal(2m, InvokePrivateMethod<decimal>(_multiplyNode, "DoDecimal", 1m, 2m));
		}
	}
}
