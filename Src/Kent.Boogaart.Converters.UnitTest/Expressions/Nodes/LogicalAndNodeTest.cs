using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class LogicalAndNodeTest : UnitTest
	{
		private LogicalAndNode _logicalAndNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_logicalAndNode = new LogicalAndNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("&", GetPrivateMemberValue<string>(_logicalAndNode, "OperatorSymbols"));
		}

		[Fact]
		public void IsSupported_ShouldYieldTrueOnlyIfBothBooleanOrNumericalTypes()
		{
			Assert.True(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.Boolean, NodeValueType.Boolean));
			Assert.True(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.Int32, NodeValueType.Int32));
			Assert.True(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.Int16, NodeValueType.Int32));

			Assert.False(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.Boolean, NodeValueType.Int16));
			Assert.False(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.String, NodeValueType.Int16));
			Assert.False(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.String, NodeValueType.String));
			Assert.False(InvokePrivateMethod<bool>(_logicalAndNode, "IsSupported", NodeValueType.Single, NodeValueType.Single));
		}

		[Fact]
		public void DoBoolean_ShouldDoLogic()
		{
			Assert.True(InvokePrivateMethod<bool>(_logicalAndNode, "DoBoolean", true, true));
			Assert.False(InvokePrivateMethod<bool>(_logicalAndNode, "DoBoolean", false, true));
			Assert.False(InvokePrivateMethod<bool>(_logicalAndNode, "DoBoolean", true, false));
			Assert.False(InvokePrivateMethod<bool>(_logicalAndNode, "DoBoolean", false, false));
		}

		[Fact]
		public void DoByte_ShouldDoLogic()
		{
			Assert.Equal(1, InvokePrivateMethod<int>(_logicalAndNode, "DoByte", (byte) 5, (byte) 3));
		}

		[Fact]
		public void DoInt16_ShouldDoLogic()
		{
			Assert.Equal(1, InvokePrivateMethod<int>(_logicalAndNode, "DoInt16", (short) 5, (short) 3));
		}

		[Fact]
		public void DoInt32_ShouldDoLogic()
		{
			Assert.Equal(1, InvokePrivateMethod<int>(_logicalAndNode, "DoInt32", 5, 3));
		}

		[Fact]
		public void DoInt64_ShouldDoLogic()
		{
			Assert.Equal(1L, InvokePrivateMethod<long>(_logicalAndNode, "DoInt64", 5L, 3L));
		}
	}
}
