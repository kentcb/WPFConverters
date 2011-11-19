using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class AddNodeTest : WideningBinaryNodeTestBase
    {
        private AddNode addNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.addNode = new AddNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("+", GetPrivateMemberValue<string>(this.addNode, "OperatorSymbols"));
        }

        [Fact]
        public void DoString_ShouldDoConcatenation()
        {
            Assert.Equal("onetwo", InvokeDoMethod<string>(this.addNode, "DoString", "one", "two"));
        }

        [Fact]
        public void DoByte_ShouldDoAddition()
        {
            Assert.Equal(3, InvokeDoMethod<int>(this.addNode, "DoByte", (byte)1, (byte)2));
        }

        [Fact]
        public void DoInt16_ShouldDoAddition()
        {
            Assert.Equal(3, InvokeDoMethod<int>(this.addNode, "DoInt16", (short)1, (short)2));
        }

        [Fact]
        public void DoInt32_ShouldDoAddition()
        {
            Assert.Equal(3, InvokeDoMethod<int>(this.addNode, "DoInt32", 1, 2));
        }

        [Fact]
        public void DoInt64_ShouldDoAddition()
        {
            Assert.Equal(3L, InvokeDoMethod<long>(this.addNode, "DoInt64", 1L, 2L));
        }

        [Fact]
        public void DoSingle_ShouldDoAddition()
        {
            Assert.Equal(3f, InvokeDoMethod<float>(this.addNode, "DoSingle", 1f, 2f));
        }

        [Fact]
        public void DoDouble_ShouldDoAddition()
        {
            Assert.Equal(3d, InvokeDoMethod<double>(this.addNode, "DoDouble", 1d, 2d));
        }

        [Fact]
        public void DoDecimal_ShouldDoAddition()
        {
            Assert.Equal(3m, InvokeDoMethod<decimal>(this.addNode, "DoDecimal", 1m, 2m));
        }
    }
}
