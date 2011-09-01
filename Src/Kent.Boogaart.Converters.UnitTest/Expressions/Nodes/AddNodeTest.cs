using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class AddNodeTest : UnitTest
	{
		private AddNode _addNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_addNode = new AddNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("+", GetPrivateMemberValue<string>(_addNode, "OperatorSymbols"));
		}

		[Fact]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.False(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.False(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.False(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.True(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.True(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
			Assert.True(InvokePrivateMethod<bool>(_addNode, "IsSupported", NodeValueType.String, NodeValueType.String));
		}

		[Fact]
		public void DoString_ShouldDoConcatenation()
		{
			Assert.Equal("onetwo", InvokePrivateMethod<string>(_addNode, "DoString", "one", "two"));
		}

		[Fact]
		public void DoByte_ShouldDoAddition()
		{
			Assert.Equal(3, InvokePrivateMethod<int>(_addNode, "DoByte", (byte) 1, (byte) 2));
		}

		[Fact]
		public void DoInt16_ShouldDoAddition()
		{
			Assert.Equal(3, InvokePrivateMethod<int>(_addNode, "DoInt16", (short) 1, (short) 2));
		}

		[Fact]
		public void DoInt32_ShouldDoAddition()
		{
			Assert.Equal(3, InvokePrivateMethod<int>(_addNode, "DoInt32", 1, 2));
		}

		[Fact]
		public void DoInt64_ShouldDoAddition()
		{
			Assert.Equal(3L, InvokePrivateMethod<long>(_addNode, "DoInt64", 1L, 2L));
		}

		[Fact]
		public void DoSingle_ShouldDoAddition()
		{
			Assert.Equal(3f, InvokePrivateMethod<float>(_addNode, "DoSingle", 1f, 2f));
		}

		[Fact]
		public void DoDouble_ShouldDoAddition()
		{
			Assert.Equal(3d, InvokePrivateMethod<double>(_addNode, "DoDouble", 1d, 2d));
		}

		[Fact]
		public void DoDecimal_ShouldDoAddition()
		{
			Assert.Equal(3m, InvokePrivateMethod<decimal>(_addNode, "DoDecimal", 1m, 2m));
		}
	}
}
