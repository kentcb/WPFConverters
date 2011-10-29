using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class DivideNodeTest : WideningBinaryNodeTestBase
	{
		private DivideNode _divideNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_divideNode = new DivideNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("/", GetPrivateMemberValue<string>(_divideNode, "OperatorSymbols"));
		}

		[Fact]
		public void DoByte_ShouldDoSubtraction()
		{
            Assert.Equal(2, InvokeDoMethod<int>(_divideNode, "DoByte", (byte)4, (byte)2));
		}

		[Fact]
		public void DoInt16_ShouldDoSubtraction()
		{
            Assert.Equal(2, InvokeDoMethod<int>(_divideNode, "DoInt16", (short)4, (short)2));
		}

		[Fact]
		public void DoInt32_ShouldDoSubtraction()
		{
            Assert.Equal(2, InvokeDoMethod<int>(_divideNode, "DoInt32", 4, 2));
		}

		[Fact]
		public void DoInt64_ShouldDoSubtraction()
		{
            Assert.Equal(2L, InvokeDoMethod<long>(_divideNode, "DoInt64", 4L, 2L));
		}

		[Fact]
		public void DoSingle_ShouldDoSubtraction()
		{
            Assert.Equal(2f, InvokeDoMethod<float>(_divideNode, "DoSingle", 4f, 2f));
		}

		[Fact]
		public void DoDouble_ShouldDoSubtraction()
		{
            Assert.Equal(2d, InvokeDoMethod<double>(_divideNode, "DoDouble", 4d, 2d));
		}

		[Fact]
		public void DoDecimal_ShouldDoSubtraction()
		{
            Assert.Equal(2m, InvokeDoMethod<decimal>(_divideNode, "DoDecimal", 4m, 2m));
		}
	}
}
