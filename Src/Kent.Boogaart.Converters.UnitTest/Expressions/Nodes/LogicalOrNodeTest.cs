using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class LogicalOrNodeTest : WideningBinaryNodeTestBase
    {
        private LogicalOrNode _logicalOrNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _logicalOrNode = new LogicalOrNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("|", GetPrivateMemberValue<string>(_logicalOrNode, "OperatorSymbols"));
        }

        [Fact]
        public void DoBoolean_ShouldDoLogic()
        {
            Assert.True(InvokeDoMethod<bool>(_logicalOrNode, "DoBoolean", true, true));
            Assert.True(InvokeDoMethod<bool>(_logicalOrNode, "DoBoolean", false, true));
            Assert.True(InvokeDoMethod<bool>(_logicalOrNode, "DoBoolean", true, false));
            Assert.False(InvokeDoMethod<bool>(_logicalOrNode, "DoBoolean", false, false));
        }

        [Fact]
        public void DoByte_ShouldDoLogic()
        {
            Assert.Equal(7, InvokeDoMethod<int>(_logicalOrNode, "DoByte", (byte)5, (byte)3));
        }

        [Fact]
        public void DoInt16_ShouldDoLogic()
        {
            Assert.Equal(7, InvokeDoMethod<int>(_logicalOrNode, "DoInt16", (short)5, (short)3));
        }

        [Fact]
        public void DoInt32_ShouldDoLogic()
        {
            Assert.Equal(7, InvokeDoMethod<int>(_logicalOrNode, "DoInt32", 5, 3));
        }

        [Fact]
        public void DoInt64_ShouldDoLogic()
        {
            Assert.Equal(7L, InvokeDoMethod<long>(_logicalOrNode, "DoInt64", 5L, 3L));
        }
    }
}
