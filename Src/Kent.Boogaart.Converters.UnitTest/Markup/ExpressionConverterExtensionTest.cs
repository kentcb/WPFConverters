using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Markup;

namespace Kent.Boogaart.Converters.UnitTest.Markup
{
	[TestFixture]
	public sealed class ExpressionConverterExtensionTest : UnitTest
	{
		private ExpressionConverterExtension _expressionConverterExtension;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_expressionConverterExtension = new ExpressionConverterExtension();
		}

		[Test]
		public void Constructor_ShouldSetDefaults()
		{
			Assert.IsNull(_expressionConverterExtension.Expression);
		}

		[Test]
		public void Constructor_Expression_ShouldSetExpression()
		{
			_expressionConverterExtension = new ExpressionConverterExtension("expr");
			Assert.AreEqual("expr", _expressionConverterExtension.Expression);
		}

		[Test]
		public void Expression_ShouldGetAndSet()
		{
			Assert.IsNull(_expressionConverterExtension.Expression);
			_expressionConverterExtension.Expression = "expr";
			Assert.AreEqual("expr", _expressionConverterExtension.Expression);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage="No expression has been provided.")]
		public void ProvideValue_ShouldThrowIfNoExpression()
		{
			_expressionConverterExtension.ProvideValue(null);
		}

		[Test]
		public void ProvideValue_ShouldYieldExpressionConverterWithGivenExpression()
		{
			_expressionConverterExtension.Expression = "324 * 21 / {0}";
			ExpressionConverter expressionConverter = _expressionConverterExtension.ProvideValue(null) as ExpressionConverter;
			Assert.IsNotNull(expressionConverter);
			Assert.AreEqual("324 * 21 / {0}", expressionConverter.Expression);
		}
	}
}
