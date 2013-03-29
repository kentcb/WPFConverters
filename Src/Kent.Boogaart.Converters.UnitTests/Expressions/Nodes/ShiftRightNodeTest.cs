using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions.Nodes
{
    public sealed class ShiftRightNodeTest : UnitTest
    {
        private ShiftRightNode shiftRightNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.shiftRightNode = new ShiftRightNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal(">>", GetPrivateMemberValue<string>(this.shiftRightNode, "OperatorSymbols"));
        }

        [Fact]
        public void DoByte_ShouldShiftRight()
        {
            Assert.Equal(4, InvokePrivateMethod<int>(this.shiftRightNode, "DoByte", (byte)16, 2));
        }

        [Fact]
        public void DoInt16_ShouldShiftRight()
        {
            Assert.Equal(4, InvokePrivateMethod<int>(this.shiftRightNode, "DoInt16", (short)16, 2));
        }

        [Fact]
        public void DoInt32_ShouldShiftRight()
        {
            Assert.Equal(4, InvokePrivateMethod<int>(this.shiftRightNode, "DoInt32", 16, 2));
        }

        [Fact]
        public void DoInt64_ShouldShiftRight()
        {
            Assert.Equal(4L, InvokePrivateMethod<long>(this.shiftRightNode, "DoInt64", 16L, 2));
        }
    }
}
