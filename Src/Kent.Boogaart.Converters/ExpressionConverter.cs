namespace Kent.Boogaart.Converters
{
    using Kent.Boogaart.Converters.Expressions;
    using Kent.Boogaart.Converters.Expressions.Nodes;
    using Kent.Boogaart.HelperTrinity;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> and <see cref="IMultiValueConverter"/> that converts bound values by using
    /// a C#-like expression.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The expression can be any valid C# expression as long as it uses only the operators discussed below. Bound values can be accessed
    /// within the expression by using curly brackets. For example, an expression of <c>{0}</c> will simply yield the first bound value
    /// unchanged, whereas an expression of <c>{0} * 2</c> will return the value doubled.
    /// </para>
    /// <para>
    /// The operators supported are listed below along with example usage. They are listed in decreasing order of precedence.
    /// <list type="table">
    ///     <listheader>
    ///         <term>Operator Category</term>
    ///         <term>Operator</term>
    ///         <term>Example</term>
    ///     </listheader>
    ///     <item>
    ///         <term>Unary</term>
    ///         <term>+</term>
    ///         <term>+4</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>-</term>
    ///         <term>-4</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>!</term>
    ///         <term>!true</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>~</term>
    ///         <term>~21</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>(T)</term>
    ///         <term>(long) 3</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>true</term>
    ///         <term>{0} == true</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>false</term>
    ///         <term>{0} == false</term>
    ///     </item>
    ///     <item>
    ///         <term>Multiplicative</term>
    ///         <term>*</term>
    ///         <term>4 * {0}</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>/</term>
    ///         <term>{0} / 4</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>%</term>
    ///         <term>{0} % 4</term>
    ///     </item>
    ///     <item>
    ///         <term>Additive</term>
    ///         <term>+</term>
    ///         <term>{0} + 4</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>-</term>
    ///         <term>{0} - 4</term>
    ///     </item>
    ///     <item>
    ///         <term>Shift</term>
    ///         <term>&lt;&lt;</term>
    ///         <term>{0} &lt;&lt; 4</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>&gt;&gt;</term>
    ///         <term>{0} &gt;&gt; 4</term>
    ///     </item>
    ///     <item>
    ///         <term>Relational</term>
    ///         <term>&lt;</term>
    ///         <term>{0} &lt; 50</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>&gt;</term>
    ///         <term>{0} &gt; 50</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>&lt;=</term>
    ///         <term>{0} &lt;= 50</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>&gt;=</term>
    ///         <term>{0} &gt;= 50</term>
    ///     </item>
    ///     <item>
    ///         <term>Equality</term>
    ///         <term>==</term>
    ///         <term>{0} == 50</term>
    ///     </item>
    ///     <item>
    ///         <term></term>
    ///         <term>!=</term>
    ///         <term>{0} != 50</term>
    ///     </item>
    ///     <item>
    ///         <term>Logical AND</term>
    ///         <term>&amp;</term>
    ///         <term>{0} &amp; 16</term>
    ///     </item>
    ///     <item>
    ///         <term>Logical OR</term>
    ///         <term>|</term>
    ///         <term>{0} | 16</term>
    ///     </item>
    ///     <item>
    ///         <term>Logical XOR</term>
    ///         <term>^</term>
    ///         <term>{0} ^ 16</term>
    ///     </item>
    ///     <item>
    ///         <term>Conditional AND</term>
    ///         <term>&amp;&amp;</term>
    ///         <term>{0} &amp;&amp; {1}</term>
    ///     </item>
    ///     <item>
    ///         <term>Conditional OR</term>
    ///         <term>||</term>
    ///         <term>{0} || {1}</term>
    ///     </item>
    ///     <item>
    ///         <term>Ternary Conditional</term>
    ///         <term>?:</term>
    ///         <term>{0} ? "yes" : "no"</term>
    ///     </item>
    ///     <item>
    ///         <term>Null Coalescing</term>
    ///         <term>??</term>
    ///         <term>{0} ?? {1} ?? "default"</term>
    ///     </item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example shows how to use an expression converter to display the value of a slider multiplied by 2. Note that
    /// the expression must be escaped with &quot;{}&quot; because it includes the '{' and '}' characters. The expression on its own
    /// is &quot;{0} * 2&quot;.
    /// <code lang="xml">
    /// <![CDATA[
    /// <StackPanel>
    ///     <Slider x:Name="_slider"/>
    ///     <Label Content="{Binding Value, ElementName=_slider}"/>
    ///     <Label Content="{Binding Value, ElementName=_slider, Converter={ExpressionConverter {}{0} * 2}}"/>
    /// </StackPanel>
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// The following example shows how to position a <c>Popup</c> exactly in the center of a hosting panel. A <see cref="MultiBinding"/>
    /// is used to perform a calculation on the widths and heights of the containing panel and the popup itself.
    /// <code lang="xml">
    /// <![CDATA[
    /// <StackPanel x:Name="_panel">
    ///     <Popup IsOpen="True" Placement="Relative" PlacementTarget="{Binding ElementName=_panel}">
    ///         <Popup.HorizontalOffset>
    ///             <MultiBinding Converter="{ExpressionConverter {}{0} / 2 - {1} / 2}">
    ///                 <Binding Path="ActualWidth" ElementName="_panel"/>
    ///                 <Binding Path="ActualWidth" ElementName="_border"/>
    ///             </MultiBinding>
    ///         </Popup.HorizontalOffset>
    ///         <Popup.VerticalOffset>
    ///             <MultiBinding Converter="{ExpressionConverter {}{0} / 2 - {1} / 2}">
    ///                 <Binding Path="ActualHeight" ElementName="_panel"/>
    ///                 <Binding Path="ActualHeight" ElementName="_border"/>
    ///             </MultiBinding>
    ///         </Popup.VerticalOffset>
    /// 
    ///         <Border x:Name="_border" Background="White" BorderThickness="1">
    ///             <Label>Here is the popup.</Label>
    ///         </Border>
    ///     </Popup>
    /// </StackPanel>
    /// ]]>
    /// </code>
    /// </example>
    [ContentProperty("Expression")]
#if !SILVERLIGHT
    [ValueConversion(typeof(object), typeof(object))]
#endif
    public sealed class ExpressionConverter : IValueConverter
#if !SILVERLIGHT
, IMultiValueConverter
#endif
    {
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(ExpressionConverter));
        private string expression;
        private Node expressionNode;

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
                return this.expression;
            }

            set
            {
                this.expression = value;
                this.expressionNode = null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the ExpressionConverter class.
        /// </summary>
        public ExpressionConverter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ExpressionConverter class with the specified expression.
        /// </summary>
        /// <param name="expression">
        /// The expression (see <see cref="Expression"/>).
        /// </param>
        public ExpressionConverter(string expression)
        {
            this.Expression = expression;
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
            this.EnsureExpressionNode();
            exceptionHelper.ResolveAndThrowIf(this.expressionNode == null, "NoExpression");
            return this.expressionNode.Evaluate(new NodeEvaluationContext(new object[] { value }));
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
            this.EnsureExpressionNode();
            exceptionHelper.ResolveAndThrowIf(this.expressionNode == null, "NoExpression");
            return this.expressionNode.Evaluate(new NodeEvaluationContext(values));
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

        // turn the expression into an AST each time it changes (not each time our Convert methods are called)
        private void EnsureExpressionNode()
        {
            if (this.expressionNode != null || string.IsNullOrEmpty(this.expression))
            {
                return;
            }

            using (var stringReader = new StringReader(this.expression))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                this.expressionNode = parser.ParseExpression();
            }
        }
    }
}