using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class CastNodeTest : UnitTest
	{
		private CastNode _castNode;

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage="Cannot convert type 'Int32' to type 'String'.")]
		public void Evaluate_ShouldThrowIfTargetTypeIsNonNumerical()
		{
			_castNode = new CastNode(new ConstantNode<int>(1), NodeValueType.String);
			_castNode.Evaluate(NodeEvaluationContext.Empty);
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Cannot convert type 'String' to type 'Int32'.")]
		public void Evaluate_ShouldThrowIfValueTypeIsNonNumerical()
		{
			_castNode = new CastNode(new ConstantNode<string>("abc"), NodeValueType.Int32);
			_castNode.Evaluate(NodeEvaluationContext.Empty);
		}

		[Test]
		public void Evaluate_ShouldCastBooleans()
		{
			_castNode = new CastNode(new ConstantNode<bool>(true), NodeValueType.Boolean);
			Assert.AreEqual(true, _castNode.Evaluate(NodeEvaluationContext.Empty));
		}

		[Test]
		public void Evaluate_ShouldCastStrings()
		{
			_castNode = new CastNode(new ConstantNode<string>("str"), NodeValueType.String);
			Assert.AreEqual("str", _castNode.Evaluate(NodeEvaluationContext.Empty));
		}

		[Test]
		public void Evaluate_ShouldCastBytes()
		{
			DoCastTests<byte>();
		}

		[Test]
		public void Evaluate_ShouldCastInt16s()
		{
			DoCastTests<short>();
		}

		[Test]
		public void Evaluate_ShouldCastInt32s()
		{
			DoCastTests<int>();
		}

		[Test]
		public void Evaluate_ShouldCastInt64s()
		{
			DoCastTests<long>();
		}

		[Test]
		public void Evaluate_ShouldCastSingles()
		{
			DoCastTests<float>();
		}

		[Test]
		public void Evaluate_ShouldCastDoubles()
		{
			DoCastTests<double>();
		}

		[Test]
		public void Evaluate_ShouldCastDecimals()
		{
			DoCastTests<decimal>();
		}

		private void DoCastTests<T>()
		{
			T value = (T) (3 as IConvertible).ToType(typeof(T), null);

			_castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Byte);
			Assert.AreEqual((byte) 3, _castNode.Evaluate(NodeEvaluationContext.Empty));

			_castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Int16);
			Assert.AreEqual((short) 3, _castNode.Evaluate(NodeEvaluationContext.Empty));

			_castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Int32);
			Assert.AreEqual(3, _castNode.Evaluate(NodeEvaluationContext.Empty));

			_castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Int64);
			Assert.AreEqual(3L, _castNode.Evaluate(NodeEvaluationContext.Empty));

			_castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Single);
			Assert.AreEqual(3f, _castNode.Evaluate(NodeEvaluationContext.Empty));

			_castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Double);
			Assert.AreEqual(3d, _castNode.Evaluate(NodeEvaluationContext.Empty));

			_castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Decimal);
			Assert.AreEqual(3m, _castNode.Evaluate(NodeEvaluationContext.Empty));
		}
	}
}
