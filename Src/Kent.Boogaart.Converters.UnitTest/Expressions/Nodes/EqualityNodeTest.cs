using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class EqualityNodeTest : UnitTest
	{
		private EqualityNode _equalityNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_equalityNode = new EqualityNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("==", GetPrivateMemberValue<string>(_equalityNode, "OperatorSymbols"));
		}

		[Fact]
		public void IsSupported_ShouldYieldTrueIfBothStringsOrBothNumeric()
		{
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.String, NodeValueType.String));
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.String, NodeValueType.Null));
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Null, NodeValueType.String));
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Null, NodeValueType.Null));
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
			Assert.False(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.False(InvokePrivateMethod<bool>(_equalityNode, "IsSupported", NodeValueType.Unknown, NodeValueType.Int32));
		}

		[Fact]
		public void DoString_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "DoString", "abc", "abc"));
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "DoString", null, null));
			Assert.False(InvokePrivateMethod<bool>(_equalityNode, "DoString", "abc", null));
			Assert.False(InvokePrivateMethod<bool>(_equalityNode, "DoString", "abc", "abcd"));
		}

		[Fact]
		public void DoBoolean_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "DoBoolean", true, true));
			Assert.False(InvokePrivateMethod<bool>(_equalityNode, "DoBoolean", true, false));
		}

		[Fact]
		public void DoByte_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "DoByte", (byte) 1, (byte) 1));
			Assert.False(InvokePrivateMethod<bool>(_equalityNode, "DoByte", (byte) 1, (byte) 2));
		}

		[Fact]
		public void DoInt16_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "DoInt16", (short) 1, (short) 1));
			Assert.False(InvokePrivateMethod<bool>(_equalityNode, "DoInt16", (short) 1, (short) 2));
		}

		[Fact]
		public void DoInt32_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "DoInt32", 1, 1));
			Assert.False(InvokePrivateMethod<bool>(_equalityNode, "DoInt32", 1, 2));
		}

		[Fact]
		public void DoInt64_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "DoInt64", 1L, 1L));
			Assert.False(InvokePrivateMethod<bool>(_equalityNode, "DoInt64", 1L, 2L));
		}

		[Fact]
		public void DoSingle_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "DoSingle", 1f, 1f));
			Assert.False(InvokePrivateMethod<bool>(_equalityNode, "DoSingle", 1f, 2f));
		}

		[Fact]
		public void DoDouble_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "DoDouble", 1d, 1d));
			Assert.False(InvokePrivateMethod<bool>(_equalityNode, "DoDouble", 1d, 2d));
		}

		[Fact]
		public void DoDecimal_ShouldDoComparison()
		{
			Assert.True(InvokePrivateMethod<bool>(_equalityNode, "DoDecimal", 1m, 1m));
			Assert.False(InvokePrivateMethod<bool>(_equalityNode, "DoDecimal", 1m, 2m));
		}
	}
}
