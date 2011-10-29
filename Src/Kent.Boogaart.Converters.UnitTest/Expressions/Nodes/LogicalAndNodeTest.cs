using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class LogicalAndNodeTest : WideningBinaryNodeTestBase
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
        public void DoBoolean_ShouldDoLogic()
        {
            Assert.True(InvokeDoMethod<bool>(_logicalAndNode, "DoBoolean", true, true));
            Assert.False(InvokeDoMethod<bool>(_logicalAndNode, "DoBoolean", false, true));
            Assert.False(InvokeDoMethod<bool>(_logicalAndNode, "DoBoolean", true, false));
            Assert.False(InvokeDoMethod<bool>(_logicalAndNode, "DoBoolean", false, false));
        }

        [Fact]
        public void DoByte_ShouldDoLogic()
        {
            Assert.Equal(1, InvokeDoMethod<int>(_logicalAndNode, "DoByte", (byte)5, (byte)3));
        }

        [Fact]
        public void DoInt16_ShouldDoLogic()
        {
            Assert.Equal(1, InvokeDoMethod<int>(_logicalAndNode, "DoInt16", (short)5, (short)3));
        }

        [Fact]
        public void DoInt32_ShouldDoLogic()
        {
            Assert.Equal(1, InvokeDoMethod<int>(_logicalAndNode, "DoInt32", 5, 3));
        }

        [Fact]
        public void DoInt64_ShouldDoLogic()
        {
            Assert.Equal(1L, InvokeDoMethod<long>(_logicalAndNode, "DoInt64", 5L, 3L));
        }
    }
}
