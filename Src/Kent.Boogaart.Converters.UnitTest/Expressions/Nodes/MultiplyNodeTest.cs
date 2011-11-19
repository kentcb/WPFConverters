using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class MultiplyNodeTest : WideningBinaryNodeTestBase
    {
        private MultiplyNode multiplyNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.multiplyNode = new MultiplyNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("*", GetPrivateMemberValue<string>(this.multiplyNode, "OperatorSymbols"));
        }

        [Fact]
        public void DoByte_ShouldDoMultiplication()
        {
            Assert.Equal(2, InvokeDoMethod<int>(this.multiplyNode, "DoByte", (byte)1, (byte)2));
        }

        [Fact]
        public void DoInt16_ShouldDoMultiplication()
        {
            Assert.Equal(2, InvokeDoMethod<int>(this.multiplyNode, "DoInt16", (short)1, (short)2));
        }

        [Fact]
        public void DoInt32_ShouldDoMultiplication()
        {
            Assert.Equal(2, InvokeDoMethod<int>(this.multiplyNode, "DoInt32", 1, 2));
        }

        [Fact]
        public void DoInt64_ShouldDoMultiplication()
        {
            Assert.Equal(2L, InvokeDoMethod<long>(this.multiplyNode, "DoInt64", 1L, 2L));
        }

        [Fact]
        public void DoSingle_ShouldDoMultiplication()
        {
            Assert.Equal(2f, InvokeDoMethod<float>(this.multiplyNode, "DoSingle", 1f, 2f));
        }

        [Fact]
        public void DoDouble_ShouldDoMultiplication()
        {
            Assert.Equal(2d, InvokeDoMethod<double>(this.multiplyNode, "DoDouble", 1d, 2d));
        }

        [Fact]
        public void DoDecimal_ShouldDoMultiplication()
        {
            Assert.Equal(2m, InvokeDoMethod<decimal>(this.multiplyNode, "DoDecimal", 1m, 2m));
        }
    }
}
