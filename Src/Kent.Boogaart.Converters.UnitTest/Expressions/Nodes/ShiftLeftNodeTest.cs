using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class ShiftLeftNodeTest : UnitTest
    {
        private ShiftLeftNode shiftLeftNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.shiftLeftNode = new ShiftLeftNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("<<", GetPrivateMemberValue<string>(this.shiftLeftNode, "OperatorSymbols"));
        }

        [Fact]
        public void DoByte_ShouldShiftLeft()
        {
            Assert.Equal(4, InvokePrivateMethod<int>(this.shiftLeftNode, "DoByte", (byte)1, 2));
        }

        [Fact]
        public void DoInt16_ShouldShiftLeft()
        {
            Assert.Equal(4, InvokePrivateMethod<int>(this.shiftLeftNode, "DoInt16", (short)1, 2));
        }

        [Fact]
        public void DoInt32_ShouldShiftLeft()
        {
            Assert.Equal(4, InvokePrivateMethod<int>(this.shiftLeftNode, "DoInt32", 1, 2));
        }

        [Fact]
        public void DoInt64_ShouldShiftLeft()
        {
            Assert.Equal(4L, InvokePrivateMethod<long>(this.shiftLeftNode, "DoInt64", 1L, 2));
        }
    }
}
