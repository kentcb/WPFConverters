using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class ModulusNodeTest : WideningBinaryNodeTestBase
    {
        private ModulusNode _modulusNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _modulusNode = new ModulusNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("%", GetPrivateMemberValue<string>(_modulusNode, "OperatorSymbols"));
        }

        [Fact]
        public void DoByte_ShouldDoModulus()
        {
            Assert.Equal(1, InvokeDoMethod<int>(_modulusNode, "DoByte", (byte)5, (byte)2));
        }

        [Fact]
        public void DoInt16_ShouldDoModulus()
        {
            Assert.Equal(1, InvokeDoMethod<int>(_modulusNode, "DoInt16", (short)5, (short)2));
        }

        [Fact]
        public void DoInt32_ShouldDoModulus()
        {
            Assert.Equal(1, InvokeDoMethod<int>(_modulusNode, "DoInt32", 5, 2));
        }

        [Fact]
        public void DoInt64_ShouldDoModulus()
        {
            Assert.Equal(1L, InvokeDoMethod<long>(_modulusNode, "DoInt64", 5L, 2L));
        }

        [Fact]
        public void DoSingle_ShouldDoModulus()
        {
            Assert.Equal(1f, InvokeDoMethod<float>(_modulusNode, "DoSingle", 5f, 2f));
        }

        [Fact]
        public void DoDouble_ShouldDoModulus()
        {
            Assert.Equal(1d, InvokeDoMethod<double>(_modulusNode, "DoDouble", 5d, 2d));
        }

        [Fact]
        public void DoDecimal_ShouldDoModulus()
        {
            Assert.Equal(1m, InvokeDoMethod<decimal>(_modulusNode, "DoDecimal", 5m, 2m));
        }
    }
}
