using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class ShiftLeftNodeTest : UnitTest
	{
		private ShiftLeftNode _shiftLeftNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_shiftLeftNode = new ShiftLeftNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal("<<", GetPrivateMemberValue<string>(_shiftLeftNode, "OperatorSymbols"));
		}

		[Fact]
		public void DoByte_ShouldShiftLeft()
		{
			Assert.Equal(4, InvokePrivateMethod<int>(_shiftLeftNode, "DoByte", (byte) 1, 2));
		}

		[Fact]
		public void DoInt16_ShouldShiftLeft()
		{
			Assert.Equal(4, InvokePrivateMethod<int>(_shiftLeftNode, "DoInt16", (short) 1, 2));
		}

		[Fact]
		public void DoInt32_ShouldShiftLeft()
		{
			Assert.Equal(4, InvokePrivateMethod<int>(_shiftLeftNode, "DoInt32", 1, 2));
		}

		[Fact]
		public void DoInt64_ShouldShiftLeft()
		{
			Assert.Equal(4L, InvokePrivateMethod<long>(_shiftLeftNode, "DoInt64", 1L, 2));
		}
	}
}
