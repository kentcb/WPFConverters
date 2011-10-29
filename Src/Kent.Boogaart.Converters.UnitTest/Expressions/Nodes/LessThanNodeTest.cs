using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class LessThanNodeTest : WideningBinaryNodeTestBase
	{
		private LessThanNode _lessThanNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_lessThanNode = new LessThanNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("<", GetPrivateMemberValue<string>(_lessThanNode, "OperatorSymbols"));
		}

		[Fact]
		public void DoByte_ShouldDoComparison()
		{
			Assert.True(InvokeDoMethod<bool>(_lessThanNode, "DoByte", (byte) 1, (byte) 2));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoByte", (byte)2, (byte)2));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoByte", (byte)3, (byte)2));
		}

		[Fact]
		public void DoInt16_ShouldDoComparison()
		{
            Assert.True(InvokeDoMethod<bool>(_lessThanNode, "DoInt16", (short)1, (short)2));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoInt16", (short)2, (short)2));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoInt16", (short)3, (short)2));
		}

		[Fact]
		public void DoInt32_ShouldDoComparison()
		{
            Assert.True(InvokeDoMethod<bool>(_lessThanNode, "DoInt32", 1, 2));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoInt32", 2, 2));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoInt32", 3, 2));
		}

		[Fact]
		public void DoInt64_ShouldDoComparison()
		{
            Assert.True(InvokeDoMethod<bool>(_lessThanNode, "DoInt64", 1L, 2L));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoInt64", 2L, 2L));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoInt64", 3L, 2L));
		}

		[Fact]
		public void DoSingle_ShouldDoComparison()
		{
            Assert.True(InvokeDoMethod<bool>(_lessThanNode, "DoSingle", 1f, 2f));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoSingle", 2f, 2f));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoSingle", 3f, 2f));
		}

		[Fact]
		public void DoDouble_ShouldDoComparison()
		{
            Assert.True(InvokeDoMethod<bool>(_lessThanNode, "DoDouble", 1d, 2d));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoDouble", 2d, 2d));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoDouble", 3d, 2d));
		}

		[Fact]
		public void DoDecimal_ShouldDoComparison()
		{
            Assert.True(InvokeDoMethod<bool>(_lessThanNode, "DoDecimal", 1m, 2m));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoDecimal", 2m, 2m));
            Assert.False(InvokeDoMethod<bool>(_lessThanNode, "DoDecimal", 3m, 2m));
		}
	}
}
