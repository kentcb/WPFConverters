using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class WideningBinaryNodeTest : UnitTest
	{
		private MockWideningBinaryNode _wideningBinaryNode;

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Operator 'op' cannot be applied to operands of type 'Int32' and 'Int16'.")]
		public void Evaluate_ShouldThrowIfUnsupportedOperands()
		{
			_wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<int>(0), new ConstantNode<short>(0));
			_wideningBinaryNode.IsSupportedValue = false;
			_wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
		}

		[Test]
		public void Evaluate_String_ShouldCallDoString()
		{
			_wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<string>(""), new ConstantNode<string>(""));
			_wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
			Assert.IsTrue(_wideningBinaryNode.DoStringCalled);
		}

		[Test]
		public void Evaluate_String_ShouldNotWidenIfOneArgumentIsNull()
		{
			_wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<string>(""), NullNode.Instance);
			_wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
			Assert.IsTrue(_wideningBinaryNode.DoStringCalled);
		}

		[Test]
		public void Evaluate_Boolean_ShouldCallDoBoolean()
		{
			_wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<bool>(false), new ConstantNode<bool>(false));
			_wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
			Assert.IsTrue(_wideningBinaryNode.DoBooleanCalled);
		}

		[Test]
		public void Evaluate_Byte_ShouldCallDoByte()
		{
			_wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<byte>(0), new ConstantNode<byte>(0));
			_wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
			Assert.IsTrue(_wideningBinaryNode.DoByteCalled);
		}

		[Test]
		public void Evaluate_Int16_ShouldCallDoInt16()
		{
			_wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<short>(0), new ConstantNode<short>(0));
			_wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
			Assert.IsTrue(_wideningBinaryNode.DoInt16Called);
		}

		[Test]
		public void Evaluate_Int32_ShouldCallDoInt32()
		{
			_wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
			_wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
			Assert.IsTrue(_wideningBinaryNode.DoInt32Called);
		}

		[Test]
		public void Evaluate_Int64_ShouldCallDoInt64()
		{
			_wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<long>(0), new ConstantNode<long>(0));
			_wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
			Assert.IsTrue(_wideningBinaryNode.DoInt64Called);
		}

		[Test]
		public void Evaluate_Single_ShouldCallDoSingle()
		{
			_wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<float>(0), new ConstantNode<float>(0));
			_wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
			Assert.IsTrue(_wideningBinaryNode.DoSingleCalled);
		}

		[Test]
		public void Evaluate_Double_ShouldCallDoDouble()
		{
			_wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<double>(0), new ConstantNode<double>(0));
			_wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
			Assert.IsTrue(_wideningBinaryNode.DoDoubleCalled);
		}

		[Test]
		public void Evaluate_Decimal_ShouldCallDoDecimal()
		{
			_wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<decimal>(0), new ConstantNode<decimal>(0));
			_wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
			Assert.IsTrue(_wideningBinaryNode.DoDecimalCalled);
		}

		#region Supporting Types

		//cannot mock because WideningBinaryNode is internal
		private sealed class MockWideningBinaryNode : WideningBinaryNode
		{
			public bool IsSupportedValue = true;
			public bool DoBooleanCalled;
			public bool DoStringCalled;
			public bool DoByteCalled;
			public bool DoInt16Called;
			public bool DoInt32Called;
			public bool DoInt64Called;
			public bool DoSingleCalled;
			public bool DoDoubleCalled;
			public bool DoDecimalCalled;

			protected override string OperatorSymbols
			{
				get
				{
					return "op";
				}
			}

			public MockWideningBinaryNode(Node leftNode, Node rightNode)
				: base(leftNode, rightNode)
			{
			}

			protected override bool IsSupported(NodeValueType leftNodeValueType, NodeValueType rightNodeValueType)
			{
				return IsSupportedValue;
			}

			protected override object DoBoolean(bool value1, bool value2)
			{
				DoBooleanCalled = true;
				return false;
			}

			protected override object DoString(string value1, string value2)
			{
				DoStringCalled = true;
				return "";
			}

			protected override object DoByte(byte value1, byte value2)
			{
				DoByteCalled = true;
				return 0;
			}

			protected override object DoInt16(short value1, short value2)
			{
				DoInt16Called = true;
				return 0;
			}

			protected override object DoInt32(int value1, int value2)
			{
				DoInt32Called = true;
				return 0;
			}

			protected override object DoInt64(long value1, long value2)
			{
				DoInt64Called = true;
				return 0;
			}

			protected override object DoSingle(float value1, float value2)
			{
				DoSingleCalled = true;
				return 0;
			}

			protected override object DoDouble(double value1, double value2)
			{
				DoDoubleCalled = true;
				return 0;
			}

			protected override object DoDecimal(decimal value1, decimal value2)
			{
				DoDecimalCalled = true;
				return 0;
			}
		}

		#endregion
	}
}
