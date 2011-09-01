using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class LogicalXorNodeTest : UnitTest
	{
		private LogicalXorNode _logicalXorNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_logicalXorNode = new LogicalXorNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("^", GetPrivateMemberValue<string>(_logicalXorNode, "OperatorSymbols"));
		}

		[Fact]
		public void IsSupported_ShouldYieldTrueOnlyIfBothBooleanOrNumericalTypes()
		{
			Assert.True(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.Boolean, NodeValueType.Boolean));
			Assert.True(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
			Assert.True(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.Int16, NodeValueType.Int32));

			Assert.False(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.Boolean, NodeValueType.Int16));
			Assert.False(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.String, NodeValueType.Int16));
			Assert.False(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.String, NodeValueType.String));
			Assert.False(InvokePrivateMethod<bool>(_logicalXorNode, "IsSupported", NodeValueType.Single, NodeValueType.Single));
		}

		[Fact]
		public void DoBoolean_ShouldDoLogic()
		{
			Assert.False(InvokePrivateMethod<bool>(_logicalXorNode, "DoBoolean", true, true));
			Assert.True(InvokePrivateMethod<bool>(_logicalXorNode, "DoBoolean", false, true));
			Assert.True(InvokePrivateMethod<bool>(_logicalXorNode, "DoBoolean", true, false));
			Assert.False(InvokePrivateMethod<bool>(_logicalXorNode, "DoBoolean", false, false));
		}

		[Fact]
		public void DoByte_ShouldDoLogic()
		{
			Assert.Equal(6, InvokePrivateMethod<int>(_logicalXorNode, "DoByte", (byte) 5, (byte) 3));
		}

		[Fact]
		public void DoInt16_ShouldDoLogic()
		{
			Assert.Equal(6, InvokePrivateMethod<int>(_logicalXorNode, "DoInt16", (short) 5, (short) 3));
		}

		[Fact]
		public void DoInt32_ShouldDoLogic()
		{
			Assert.Equal(6, InvokePrivateMethod<int>(_logicalXorNode, "DoInt32", 5, 3));
		}

		[Fact]
		public void DoInt64_ShouldDoLogic()
		{
			Assert.Equal(6L, InvokePrivateMethod<long>(_logicalXorNode, "DoInt64", 5L, 3L));
		}
	}
}
