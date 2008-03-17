using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class ShiftLeftNodeTest : UnitTest
	{
		private ShiftLeftNode _shiftLeftNode;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_shiftLeftNode = new ShiftLeftNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
		}

		[Test]
		public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
		{
			Assert.AreEqual("<<", GetPrivateMemberValue<string>(_shiftLeftNode, "OperatorSymbols"));
		}

		[Test]
		public void DoByte_ShouldShiftLeft()
		{
			Assert.AreEqual(4, InvokePrivateMethod<int>(_shiftLeftNode, "DoByte", (byte) 1, 2));
		}

		[Test]
		public void DoInt16_ShouldShiftLeft()
		{
			Assert.AreEqual(4, InvokePrivateMethod<int>(_shiftLeftNode, "DoInt16", (short) 1, 2));
		}

		[Test]
		public void DoInt32_ShouldShiftLeft()
		{
			Assert.AreEqual(4, InvokePrivateMethod<int>(_shiftLeftNode, "DoInt32", 1, 2));
		}

		[Test]
		public void DoInt64_ShouldShiftLeft()
		{
			Assert.AreEqual(4L, InvokePrivateMethod<long>(_shiftLeftNode, "DoInt64", 1L, 2));
		}
	}
}
