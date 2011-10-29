using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class LogicalXorNodeTest : WideningBinaryNodeTestBase
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
        public void DoBoolean_ShouldDoLogic()
        {
            Assert.False(InvokeDoMethod<bool>(_logicalXorNode, "DoBoolean", true, true));
            Assert.True(InvokeDoMethod<bool>(_logicalXorNode, "DoBoolean", false, true));
            Assert.True(InvokeDoMethod<bool>(_logicalXorNode, "DoBoolean", true, false));
            Assert.False(InvokeDoMethod<bool>(_logicalXorNode, "DoBoolean", false, false));
        }

        [Fact]
        public void DoByte_ShouldDoLogic()
        {
            Assert.Equal(6, InvokeDoMethod<int>(_logicalXorNode, "DoByte", (byte)5, (byte)3));
        }

        [Fact]
        public void DoInt16_ShouldDoLogic()
        {
            Assert.Equal(6, InvokeDoMethod<int>(_logicalXorNode, "DoInt16", (short)5, (short)3));
        }

        [Fact]
        public void DoInt32_ShouldDoLogic()
        {
            Assert.Equal(6, InvokeDoMethod<int>(_logicalXorNode, "DoInt32", 5, 3));
        }

        [Fact]
        public void DoInt64_ShouldDoLogic()
        {
            Assert.Equal(6L, InvokeDoMethod<long>(_logicalXorNode, "DoInt64", 5L, 3L));
        }
    }
}
