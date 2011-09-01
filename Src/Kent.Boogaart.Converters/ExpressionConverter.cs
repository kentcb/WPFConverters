using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Kent.Boogaart.Converters.Markup;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters
{
    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> and <see cref="IMultiValueConverter"/> that converts bound values by using
    /// an expression. See <see cref="ExpressionConverterExtension"/> for more information.
    /// </summary>
    [ContentProperty("Expression")]
#if !SILVERLIGHT
    [ValueConversion(typeof(object), typeof(object))]
#endif
    public sealed class ExpressionConverter : DependencyObject, IValueConverter
#if !SILVERLIGHT
        , IMultiValueConverter
#endif
    {
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(ExpressionConverter));
        private Node _expressionNode;

        /// <summary>
        /// Identifies the <see cref="Expression"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ExpressionProperty = DependencyProperty.Register("Expression",
            typeof(string),
            typeof(ExpressionConverter),
            new PropertyMetadata(Expression_Changed));

        /// <summary>
        /// Gets or sets the expression for this <c>MathConverter</c>.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("expression")]
#endif
        public string Expression
        {
            get
            {
                return GetValue(ExpressionProperty) as string;
            }
            set
            {
                SetValue(ExpressionProperty, value);
            }
        }

        /// <summary>
        /// Constructs an instance of <c>MathConverter</c>.
        /// </summary>
        public ExpressionConverter()
        {
        }

        /// <summary>
        /// Constructs an instance of <c>MathConverter</c> with the specified expression.
        /// </summary>
        /// <param name="expression">
        /// The expression (see <see cref="Expression"/>).
        /// </param>
        public ExpressionConverter(string expression)
        {
            Expression = expression;
        }

        /// <summary>
        /// Attempts to convert the specified value.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="targetType">
        /// The type of the binding target property.
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// </param>
        /// <returns>
        /// A converted value.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            exceptionHelper.ResolveAndThrowIf(_expressionNode == null, "NoExpression");
            return _expressionNode.Evaluate(new NodeEvaluationContext(new object[] { value }));
        }

        /// <summary>
        /// Attempts to convert the specified value back.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="targetType">
        /// The type of the binding target property.
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// </param>
        /// <returns>
        /// A converted value.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

#if !SILVERLIGHT
        /// <summary>
        /// Attempts to convert the specified values.
        /// </summary>
        /// <param name="values">
        /// The values to convert.
        /// </param>
        /// <param name="targetType">
        /// The type of the binding target property.
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// </param>
        /// <returns>
        /// A converted value.
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            exceptionHelper.ResolveAndThrowIf(_expressionNode == null, "NoExpression");
            return _expressionNode.Evaluate(new NodeEvaluationContext(values));
        }

        /// <summary>
        /// Attempts to convert back the specified values.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="targetTypes">
        /// The types of the binding target properties.
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// </param>
        /// <returns>
        /// Converted values.
        /// </returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
#endif

        private static void Expression_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ExpressionConverter expressionConverter = dependencyObject as ExpressionConverter;
            Debug.Assert(expressionConverter != null);

            if (expressionConverter.Expression != null)
            {
                //turn the expression into an AST each time it changes (not each time our Convert methods are called)
                using (StringReader stringReader = new StringReader(expressionConverter.Expression))
                using (Tokenizer tokenizer = new Tokenizer(stringReader))
                using (Parser parser = new Parser(tokenizer))
                {
                    expressionConverter._expressionNode = parser.ParseExpression();
                }
            }
            else
            {
                expressionConverter._expressionNode = null;
            }
        }
    }
}