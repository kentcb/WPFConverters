using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class LessThanOrEqualNodeTest : UnitTest
	{
		private LessThanOrEqualNode _lessThanOrEqualNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_lessThanOrEqualNode = new LessThanOrEqualNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("<=", GetPrivateMemberValue<string>(_lessThanOrEqualNode, "OperatorSymbols"));
		}

		[Fact]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.False(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.False(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.False(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
		}

		[Fact]
		public void DoByte_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoByte", (byte) 1, (byte) 2));
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoByte", (byte) 2, (byte) 2));
			Assert.False(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoByte", (byte) 3, (byte) 2));
		}

		[Fact]
		public void DoInt16_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt16", (short) 1, (short) 2));
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt16", (short) 2, (short) 2));
			Assert.False(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt16", (short) 3, (short) 2));
		}

		[Fact]
		public void DoInt32_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt32", 1, 2));
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt32", 2, 2));
			Assert.False(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt32", 3, 2));
		}

		[Fact]
		public void DoInt64_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt64", 1L, 2L));
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt64", 2L, 2L));
			Assert.False(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoInt64", 3L, 2L));
		}

		[Fact]
		public void DoSingle_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoSingle", 1f, 2f));
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoSingle", 2f, 2f));
			Assert.False(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoSingle", 3f, 2f));
		}

		[Fact]
		public void DoDouble_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoDouble", 1d, 2d));
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoDouble", 2d, 2d));
			Assert.False(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoDouble", 3d, 2d));
		}

		[Fact]
		public void DoDecimal_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoDecimal", 1m, 2m));
			Assert.True(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoDecimal", 2m, 2m));
			Assert.False(InvokePrivateMethod<bool>(_lessThanOrEqualNode, "DoDecimal", 3m, 2m));
		}
	}
}
