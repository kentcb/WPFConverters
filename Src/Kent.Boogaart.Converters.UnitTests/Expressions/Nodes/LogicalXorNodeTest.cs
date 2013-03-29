using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions.Nodes
{
    public sealed class LogicalXorNodeTest : WideningBinaryNodeTestBase
    {
        private LogicalXorNode logicalXorNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.logicalXorNode = new LogicalXorNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("^", GetPrivateMemberValue<string>(this.logicalXorNode, "OperatorSymbols"));
        }

        [Fact]
        public void DoBoolean_ShouldDoLogic()
        {
            Assert.False(InvokeDoMethod<bool>(this.logicalXorNode, "DoBoolean", true, true));
            Assert.True(InvokeDoMethod<bool>(this.logicalXorNode, "DoBoolean", false, true));
            Assert.True(InvokeDoMethod<bool>(this.logicalXorNode, "DoBoolean", true, false));
            Assert.False(InvokeDoMethod<bool>(this.logicalXorNode, "DoBoolean", false, false));
        }

        [Fact]
        public void DoByte_ShouldDoLogic()
        {
            Assert.Equal(6, InvokeDoMethod<int>(this.logicalXorNode, "DoByte", (byte)5, (byte)3));
        }

        [Fact]
        public void DoInt16_ShouldDoLogic()
        {
            Assert.Equal(6, InvokeDoMethod<int>(this.logicalXorNode, "DoInt16", (short)5, (short)3));
        }

        [Fact]
        public void DoInt32_ShouldDoLogic()
        {
            Assert.Equal(6, InvokeDoMethod<int>(this.logicalXorNode, "DoInt32", 5, 3));
        }

        [Fact]
        public void DoInt64_ShouldDoLogic()
        {
            Assert.Equal(6L, InvokeDoMethod<long>(this.logicalXorNode, "DoInt64", 5L, 3L));
        }
    }
}
