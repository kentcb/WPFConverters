using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions.Nodes
{
    public sealed class LogicalAndNodeTest : WideningBinaryNodeTestBase
    {
        private LogicalAndNode logicalAndNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.logicalAndNode = new LogicalAndNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("&", GetPrivateMemberValue<string>(this.logicalAndNode, "OperatorSymbols"));
        }

        [Fact]
        public void DoBoolean_ShouldDoLogic()
        {
            Assert.True(InvokeDoMethod<bool>(this.logicalAndNode, "DoBoolean", true, true));
            Assert.False(InvokeDoMethod<bool>(this.logicalAndNode, "DoBoolean", false, true));
            Assert.False(InvokeDoMethod<bool>(this.logicalAndNode, "DoBoolean", true, false));
            Assert.False(InvokeDoMethod<bool>(this.logicalAndNode, "DoBoolean", false, false));
        }

        [Fact]
        public void DoByte_ShouldDoLogic()
        {
            Assert.Equal(1, InvokeDoMethod<int>(this.logicalAndNode, "DoByte", (byte)5, (byte)3));
        }

        [Fact]
        public void DoInt16_ShouldDoLogic()
        {
            Assert.Equal(1, InvokeDoMethod<int>(this.logicalAndNode, "DoInt16", (short)5, (short)3));
        }

        [Fact]
        public void DoInt32_ShouldDoLogic()
        {
            Assert.Equal(1, InvokeDoMethod<int>(this.logicalAndNode, "DoInt32", 5, 3));
        }

        [Fact]
        public void DoInt64_ShouldDoLogic()
        {
            Assert.Equal(1L, InvokeDoMethod<long>(this.logicalAndNode, "DoInt64", 5L, 3L));
        }
    }
}
