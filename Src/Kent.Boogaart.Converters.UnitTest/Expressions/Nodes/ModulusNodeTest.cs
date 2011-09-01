using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class ModulusNodeTest : UnitTest
	{
		private ModulusNode _modulusNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_modulusNode = new ModulusNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("%", GetPrivateMemberValue<string>(_modulusNode, "OperatorSymbols"));
		}

		[Fact]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.False(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.False(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.False(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.True(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.True(InvokePrivateMethod<bool>(_modulusNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
		}

		[Fact]
		public void DoByte_ShouldDoModulus()
		{
			Assert.Equal(1, InvokePrivateMethod<int>(_modulusNode, "DoByte", (byte) 5, (byte) 2));
		}

		[Fact]
		public void DoInt16_ShouldDoModulus()
		{
			Assert.Equal(1, InvokePrivateMethod<int>(_modulusNode, "DoInt16", (short) 5, (short) 2));
		}

		[Fact]
		public void DoInt32_ShouldDoModulus()
		{
			Assert.Equal(1, InvokePrivateMethod<int>(_modulusNode, "DoInt32", 5, 2));
		}

		[Fact]
		public void DoInt64_ShouldDoModulus()
		{
			Assert.Equal(1L, InvokePrivateMethod<long>(_modulusNode, "DoInt64", 5L, 2L));
		}

		[Fact]
		public void DoSingle_ShouldDoModulus()
		{
			Assert.Equal(1f, InvokePrivateMethod<float>(_modulusNode, "DoSingle", 5f, 2f));
		}

		[Fact]
		public void DoDouble_ShouldDoModulus()
		{
			Assert.Equal(1d, InvokePrivateMethod<double>(_modulusNode, "DoDouble", 5d, 2d));
		}

		[Fact]
		public void DoDecimal_ShouldDoModulus()
		{
			Assert.Equal(1m, InvokePrivateMethod<decimal>(_modulusNode, "DoDecimal", 5m, 2m));
		}
	}
}
