using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class SubtractNodeTest : WideningBinaryNodeTestBase
    {
        private SubtractNode _subtractNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _subtractNode = new SubtractNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("-", GetPrivateMemberValue<string>(_subtractNode, "OperatorSymbols"));
        }

        [Fact]
        public void DoByte_ShouldDoSubtraction()
        {
            Assert.Equal(-1, this.InvokeDoMethod<int>(_subtractNode, "DoByte", (byte)1, (byte)2));
        }

        [Fact]
        public void DoInt16_ShouldDoSubtraction()
        {
            Assert.Equal(-1, InvokeDoMethod<int>(_subtractNode, "DoInt16", (short)1, (short)2));
        }

        [Fact]
        public void DoInt32_ShouldDoSubtraction()
        {
            Assert.Equal(-1, InvokeDoMethod<int>(_subtractNode, "DoInt32", 1, 2));
        }

        [Fact]
        public void DoInt64_ShouldDoSubtraction()
        {
            Assert.Equal(-1L, InvokeDoMethod<long>(_subtractNode, "DoInt64", 1L, 2L));
        }

        [Fact]
        public void DoSingle_ShouldDoSubtraction()
        {
            Assert.Equal(-1f, InvokeDoMethod<float>(_subtractNode, "DoSingle", 1f, 2f));
        }

        [Fact]
        public void DoDouble_ShouldDoSubtraction()
        {
            Assert.Equal(-1d, InvokeDoMethod<double>(_subtractNode, "DoDouble", 1d, 2d));
        }

        [Fact]
        public void DoDecimal_ShouldDoSubtraction()
        {
            Assert.Equal(-1m, InvokeDoMethod<decimal>(_subtractNode, "DoDecimal", 1m, 2m));
        }
    }
}
