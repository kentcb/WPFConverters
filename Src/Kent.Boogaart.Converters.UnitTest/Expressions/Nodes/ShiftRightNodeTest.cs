using Xunit;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	public sealed class ShiftRightNodeTest : UnitTest
	{
		private ShiftRightNode _shiftRightNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_shiftRightNode = new ShiftRightNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Fact]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.Equal(">>", GetPrivateMemberValue<string>(_shiftRightNode, "OperatorSymbols"));
		}

		[Fact]
		public void DoByte_ShouldShiftRight()
		{
			Assert.Equal(4, InvokePrivateMethod<int>(_shiftRightNode, "DoByte", (byte) 16, 2));
		}

		[Fact]
		public void DoInt16_ShouldShiftRight()
		{
			Assert.Equal(4, InvokePrivateMethod<int>(_shiftRightNode, "DoInt16", (short) 16, 2));
		}

		[Fact]
		public void DoInt32_ShouldShiftRight()
		{
			Assert.Equal(4, InvokePrivateMethod<int>(_shiftRightNode, "DoInt32", 16, 2));
		}

		[Fact]
		public void DoInt64_ShouldShiftRight()
		{
			Assert.Equal(4L, InvokePrivateMethod<long>(_shiftRightNode, "DoInt64", 16L, 2));
		}
	}
}
