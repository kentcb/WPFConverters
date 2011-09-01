using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class DivideNodeTest : UnitTest
	{
		private DivideNode _divideNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_divideNode = new DivideNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("/", GetPrivateMemberValue<string>(_divideNode, "OperatorSymbols"));
		}

		[Fact]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.False(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.False(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.False(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.True(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.True(InvokePrivateMethod<bool>(_divideNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
		}

		[Fact]
		public void DoByte_ShouldDoSubtraction()
		{
			Assert.Equal(2, InvokePrivateMethod<int>(_divideNode, "DoByte", (byte) 4, (byte) 2));
		}

		[Fact]
		public void DoInt16_ShouldDoSubtraction()
		{
			Assert.Equal(2, InvokePrivateMethod<int>(_divideNode, "DoInt16", (short) 4, (short) 2));
		}

		[Fact]
		public void DoInt32_ShouldDoSubtraction()
		{
			Assert.Equal(2, InvokePrivateMethod<int>(_divideNode, "DoInt32", 4, 2));
		}

		[Fact]
		public void DoInt64_ShouldDoSubtraction()
		{
			Assert.Equal(2L, InvokePrivateMethod<long>(_divideNode, "DoInt64", 4L, 2L));
		}

		[Fact]
		public void DoSingle_ShouldDoSubtraction()
		{
			Assert.Equal(2f, InvokePrivateMethod<float>(_divideNode, "DoSingle", 4f, 2f));
		}

		[Fact]
		public void DoDouble_ShouldDoSubtraction()
		{
			Assert.Equal(2d, InvokePrivateMethod<double>(_divideNode, "DoDouble", 4d, 2d));
		}

		[Fact]
		public void DoDecimal_ShouldDoSubtraction()
		{
			Assert.Equal(2m, InvokePrivateMethod<decimal>(_divideNode, "DoDecimal", 4m, 2m));
		}
	}
}
