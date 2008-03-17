using System;
using System.Windows;
using NUnit.Framework;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
	[TestFixture]
	public sealed class ExpressionConverterTest : UnitTest
	{
		private ExpressionConverter _expressionConverter;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_expressionConverter = new ExpressionConverter();
		}

		[Test]
		public void Constructor_ShouldSetDefaults()
		{
			Assert.IsNull(_expressionConverter.Expression);
		}

		[Test]
		public void Constructor_Expression_ShouldSetExpression()
		{
			_expressionConverter = new ExpressionConverter(null);
			Assert.IsNull(_expressionConverter.Expression);
			_expressionConverter = new ExpressionConverter("null");
			Assert.AreEqual("null", _expressionConverter.Expression);
		}

		[Test]
		public void Expression_ShouldGetAndSetExpression()
		{
			Assert.IsNull(_expressionConverter.Expression);
			_expressionConverter.Expression = "null";
			Assert.AreEqual("null", _expressionConverter.Expression);
			_expressionConverter.Expression = null;
			Assert.IsNull(_expressionConverter.Expression);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "No Expression has been specified.")]
		public void Convert_Single_ShouldThrowIfNoExpression()
		{
			_expressionConverter.Convert(1, null, null, null);
		}

		[Test]
		public void Convert_Single_ShouldEvaluateExpression()
		{
			_expressionConverter.Expression = "2 * {0}";
			Assert.AreEqual(10, _expressionConverter.Convert(5, null, null, null));
			Assert.AreEqual(-10, _expressionConverter.Convert(-5, null, null, null));
			Assert.AreEqual(10d, _expressionConverter.Convert(5d, null, null, null));
		}

		[Test]
		public void ConvertBack_ShouldReturnUnsetValue()
		{
			Assert.AreSame(DependencyProperty.UnsetValue, _expressionConverter.ConvertBack("abc", (Type) null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _expressionConverter.ConvertBack(123, (Type) null, null, null));
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "No Expression has been specified.")]
		public void Convert_Multi_ShouldThrowIfNoExpression()
		{
			_expressionConverter.Convert(new object[] { 123, 89 }, null, null, null);
		}

		[Test]
		public void Convert_Multi_ShouldEvaluateExpression()
		{
			_expressionConverter.Expression = "2 * {0} + {1} - {2}";
			Assert.AreEqual(22, _expressionConverter.Convert(new object[] { 10, 3, 1 }, null, null, null));
			Assert.AreEqual(-22, _expressionConverter.Convert(new object[] { -10, -3, -1 }, null, null, null));
			Assert.AreEqual(22d, _expressionConverter.Convert(new object[] { 10d, 3, 1 }, null, null, null));
		}

		[Test]
		public void ConvertBack_Multi_ShouldReturnNull()
		{
			Assert.IsNull(_expressionConverter.ConvertBack(10, new Type[] { typeof(int), typeof(string) }, null, null));
		}
	}
}