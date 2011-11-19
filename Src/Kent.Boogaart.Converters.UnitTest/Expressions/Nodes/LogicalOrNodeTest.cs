using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class LogicalOrNodeTest : WideningBinaryNodeTestBase
    {
        private LogicalOrNode logicalOrNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.logicalOrNode = new LogicalOrNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("|", GetPrivateMemberValue<string>(this.logicalOrNode, "OperatorSymbols"));
        }

        [Fact]
        public void DoBoolean_ShouldDoLogic()
        {
            Assert.True(InvokeDoMethod<bool>(this.logicalOrNode, "DoBoolean", true, true));
            Assert.True(InvokeDoMethod<bool>(this.logicalOrNode, "DoBoolean", false, true));
            Assert.True(InvokeDoMethod<bool>(this.logicalOrNode, "DoBoolean", true, false));
            Assert.False(InvokeDoMethod<bool>(this.logicalOrNode, "DoBoolean", false, false));
        }

        [Fact]
        public void DoByte_ShouldDoLogic()
        {
            Assert.Equal(7, InvokeDoMethod<int>(this.logicalOrNode, "DoByte", (byte)5, (byte)3));
        }

        [Fact]
        public void DoInt16_ShouldDoLogic()
        {
            Assert.Equal(7, InvokeDoMethod<int>(this.logicalOrNode, "DoInt16", (short)5, (short)3));
        }

        [Fact]
        public void DoInt32_ShouldDoLogic()
        {
            Assert.Equal(7, InvokeDoMethod<int>(this.logicalOrNode, "DoInt32", 5, 3));
        }

        [Fact]
        public void DoInt64_ShouldDoLogic()
        {
            Assert.Equal(7L, InvokeDoMethod<long>(this.logicalOrNode, "DoInt64", 5L, 3L));
        }
    }
}
