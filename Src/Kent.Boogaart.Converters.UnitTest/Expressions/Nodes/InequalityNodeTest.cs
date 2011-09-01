using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class InequalityNodeTest : UnitTest
	{
		private InequalityNode _inequalityNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_inequalityNode = new InequalityNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("!=", GetPrivateMemberValue<string>(_inequalityNode, "OperatorSymbols"));
		}

		[Fact]
		public void IsSupported_ShouldYieldTrueIfBothStringsOrBothNumeric()
		{
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.String, NodeValueType.String));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.String, NodeValueType.Null));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Null, NodeValueType.String));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Null, NodeValueType.Null));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Int16, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Double, NodeValueType.Byte));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Decimal, NodeValueType.Decimal));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
			Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.String, NodeValueType.Int32));
			Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", NodeValueType.Unknown, NodeValueType.Int32));
		}

		[Fact]
		public void DoString_ShouldDoComparison()
		{
			Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoString", "abc", "abc"));
			Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoString", null, null));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoString", "abc", null));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoString", "abc", "abcd"));
		}

		[Fact]
		public void DoBoolean_ShouldDoComparison()
		{
			Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoBoolean", true, true));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoBoolean", true, false));
		}

		[Fact]
		public void DoByte_ShouldDoComparison()
		{
			Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoByte", (byte) 1, (byte) 1));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoByte", (byte) 1, (byte) 2));
		}

		[Fact]
		public void DoInt16_ShouldDoComparison()
		{
			Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoInt16", (short) 1, (short) 1));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoInt16", (short) 1, (short) 2));
		}

		[Fact]
		public void DoInt32_ShouldDoComparison()
		{
			Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoInt32", 1, 1));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoInt32", 1, 2));
		}

		[Fact]
		public void DoInt64_ShouldDoComparison()
		{
			Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoInt64", 1L, 1L));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoInt64", 1L, 2L));
		}

		[Fact]
		public void DoSingle_ShouldDoComparison()
		{
			Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoSingle", 1f, 1f));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoSingle", 1f, 2f));
		}

		[Fact]
		public void DoDouble_ShouldDoComparison()
		{
			Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoDouble", 1d, 1d));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoDouble", 1d, 2d));
		}

		[Fact]
		public void DoDecimal_ShouldDoComparison()
		{
			Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoDecimal", 1m, 1m));
			Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoDecimal", 1m, 2m));
		}
	}
}
