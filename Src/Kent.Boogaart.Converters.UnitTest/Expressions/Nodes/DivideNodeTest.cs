using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class DivideNodeTest : WideningBinaryNodeTestBase
    {
        private DivideNode divideNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.divideNode = new DivideNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("/", GetPrivateMemberValue<string>(this.divideNode, "OperatorSymbols"));
        }

        [Fact]
        public void DoByte_ShouldDoSubtraction()
        {
            Assert.Equal(2, InvokeDoMethod<int>(this.divideNode, "DoByte", (byte)4, (byte)2));
        }

        [Fact]
        public void DoInt16_ShouldDoSubtraction()
        {
            Assert.Equal(2, InvokeDoMethod<int>(this.divideNode, "DoInt16", (short)4, (short)2));
        }

        [Fact]
        public void DoInt32_ShouldDoSubtraction()
        {
            Assert.Equal(2, InvokeDoMethod<int>(this.divideNode, "DoInt32", 4, 2));
        }

        [Fact]
        public void DoInt64_ShouldDoSubtraction()
        {
            Assert.Equal(2L, InvokeDoMethod<long>(this.divideNode, "DoInt64", 4L, 2L));
        }

        [Fact]
        public void DoSingle_ShouldDoSubtraction()
        {
            Assert.Equal(2f, InvokeDoMethod<float>(this.divideNode, "DoSingle", 4f, 2f));
        }

        [Fact]
        public void DoDouble_ShouldDoSubtraction()
        {
            Assert.Equal(2d, InvokeDoMethod<double>(this.divideNode, "DoDouble", 4d, 2d));
        }

        [Fact]
        public void DoDecimal_ShouldDoSubtraction()
        {
            Assert.Equal(2m, InvokeDoMethod<decimal>(this.divideNode, "DoDecimal", 4m, 2m));
        }
    }
}
