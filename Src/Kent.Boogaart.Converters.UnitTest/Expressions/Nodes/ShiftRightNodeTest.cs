using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class ShiftRightNodeTest : UnitTest
	{
		private ShiftRightNode _shiftRightNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_shiftRightNode = new ShiftRightNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual(">>", GetPrivateMemberValue<string>(_shiftRightNode, "OperatorSymbols"));
		}

		[Test]
		public void DoByte_ShouldShiftRight()
		{
			Assert.AreEqual(4, InvokePrivateMethod<int>(_shiftRightNode, "DoByte", (byte) 16, 2));
		}

		[Test]
		public void DoInt16_ShouldShiftRight()
		{
			Assert.AreEqual(4, InvokePrivateMethod<int>(_shiftRightNode, "DoInt16", (short) 16, 2));
		}

		[Test]
		public void DoInt32_ShouldShiftRight()
		{
			Assert.AreEqual(4, InvokePrivateMethod<int>(_shiftRightNode, "DoInt32", 16, 2));
		}

		[Test]
		public void DoInt64_ShouldShiftRight()
		{
			Assert.AreEqual(4L, InvokePrivateMethod<long>(_shiftRightNode, "DoInt64", 16L, 2));
		}
	}
}
