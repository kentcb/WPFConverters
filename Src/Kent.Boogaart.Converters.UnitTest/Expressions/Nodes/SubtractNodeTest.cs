using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class SubtractNodeTest : UnitTest
	{
		private SubtractNode _subtractNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_subtractNode = new SubtractNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("-", GetPrivateMemberValue<string>(_subtractNode, "OperatorSymbols"));
		}

		[Fact]
		public void IsSupported_ShouldYieldTrueOnlyForNumericalTypes()
		{
			Assert.False(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.Int16, NodeValueType.Boolean));
			Assert.False(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.Int16, NodeValueType.String));
			Assert.False(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.True(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.True(InvokePrivateMethod<bool>(_subtractNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
		}

		[Fact]
		public void DoByte_ShouldDoSubtraction()
		{
			Assert.Equal(-1, InvokePrivateMethod<int>(_subtractNode, "DoByte", (byte) 1, (byte) 2));
		}

		[Fact]
		public void DoInt16_ShouldDoSubtraction()
		{
			Assert.Equal(-1, InvokePrivateMethod<int>(_subtractNode, "DoInt16", (short) 1, (short) 2));
		}

		[Fact]
		public void DoInt32_ShouldDoSubtraction()
		{
			Assert.Equal(-1, InvokePrivateMethod<int>(_subtractNode, "DoInt32", 1, 2));
		}

		[Fact]
		public void DoInt64_ShouldDoSubtraction()
		{
			Assert.Equal(-1L, InvokePrivateMethod<long>(_subtractNode, "DoInt64", 1L, 2L));
		}

		[Fact]
		public void DoSingle_ShouldDoSubtraction()
		{
			Assert.Equal(-1f, InvokePrivateMethod<float>(_subtractNode, "DoSingle", 1f, 2f));
		}

		[Fact]
		public void DoDouble_ShouldDoSubtraction()
		{
			Assert.Equal(-1d, InvokePrivateMethod<double>(_subtractNode, "DoDouble", 1d, 2d));
		}

		[Fact]
		public void DoDecimal_ShouldDoSubtraction()
		{
			Assert.Equal(-1m, InvokePrivateMethod<decimal>(_subtractNode, "DoDecimal", 1m, 2m));
		}
	}
}
