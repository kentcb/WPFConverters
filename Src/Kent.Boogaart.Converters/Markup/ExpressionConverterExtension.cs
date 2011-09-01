using System;
using System.Windows.Data;
using System.Windows.Markup;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Markup
{
#if !SILVERLIGHT
    /// <summary>
    /// Implements a markup extension that allows data to be converted based on a C#-like expression.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The expression can be any valid C# expression as long as it uses only the operators discussed below. Bound values can be accessed
    /// within the expression by using curly brackets. For example, an expression of &quot;{0}&quot; will simply yield the first bound
    /// value.
    /// </para>
    /// <para>
    /// The operators supported are listed below along with example usage. They are listed in order of precedence.
    /// <list type="table">
    ///		<listheader>
    ///			<term>Operator Category</term>
    ///			<term>Operator</term>
    ///			<term>Example</term>
    ///		</listheader>
    ///		<item>
    ///			<term>Unary</term>
    ///			<term>+</term>
    ///			<term>+4</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>-</term>
    ///			<term>-4</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>!</term>
    ///			<term>!true</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>~</term>
    ///			<term>~21</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>(T)</term>
    ///			<term>(long) 3</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>true</term>
    ///			<term>{0} == true</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>false</term>
    ///			<term>{0} == false</term>
    ///		</item>
    ///		<item>
    ///			<term>Multiplicative</term>
    ///			<term>*</term>
    ///			<term>4 * {0}</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>/</term>
    ///			<term>{0} / 4</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>%</term>
    ///			<term>{0} % 4</term>
    ///		</item>
    ///		<item>
    ///			<term>Additive</term>
    ///			<term>+</term>
    ///			<term>{0} + 4</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>-</term>
    ///			<term>{0} - 4</term>
    ///		</item>
    ///		<item>
    ///			<term>Shift</term>
    ///			<term>&lt;&lt;</term>
    ///			<term>{0} &lt;&lt; 4</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>&gt;&gt;</term>
    ///			<term>{0} &gt;&gt; 4</term>
    ///		</item>
    ///		<item>
    ///			<term>Relational</term>
    ///			<term>&lt;</term>
    ///			<term>{0} &lt; 50</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>&gt;</term>
    ///			<term>{0} &gt; 50</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>&lt;=</term>
    ///			<term>{0} &lt;= 50</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>&gt;=</term>
    ///			<term>{0} &gt;= 50</term>
    ///		</item>
    ///		<item>
    ///			<term>Equality</term>
    ///			<term>==</term>
    ///			<term>{0} == 50</term>
    ///		</item>
    ///		<item>
    ///			<term></term>
    ///			<term>!=</term>
    ///			<term>{0} != 50</term>
    ///		</item>
    ///		<item>
    ///			<term>Logical AND</term>
    ///			<term>&amp;</term>
    ///			<term>{0} &amp; 16</term>
    ///		</item>
    ///		<item>
    ///			<term>Logical OR</term>
    ///			<term>|</term>
    ///			<term>{0} | 16</term>
    ///		</item>
    ///		<item>
    ///			<term>Logical XOR</term>
    ///			<term>^</term>
    ///			<term>{0} ^ 16</term>
    ///		</item>
    ///		<item>
    ///			<term>Conditional AND</term>
    ///			<term>&amp;&amp;</term>
    ///			<term>{0} &amp;&amp; {1}</term>
    ///		</item>
    ///		<item>
    ///			<term>Conditional OR</term>
    ///			<term>||</term>
    ///			<term>{0} || {1}</term>
    ///		</item>
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
    /// 	<Label Content="{Binding Value, ElementName=_slider, Converter={ExpressionConverter {}{0} * 2}}"/>
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
    /// 	<Popup IsOpen="True" Placement="Relative" PlacementTarget="{Binding ElementName=_panel}">
    /// 		<Popup.HorizontalOffset>
    /// 			<MultiBinding Converter="{ExpressionConverter {}{0} / 2 - {1} / 2}">
    /// 				<Binding Path="ActualWidth" ElementName="_panel"/>
    /// 				<Binding Path="ActualWidth" ElementName="_border"/>
    /// 			</MultiBinding>
    /// 		</Popup.HorizontalOffset>
    /// 		<Popup.VerticalOffset>
    /// 			<MultiBinding Converter="{ExpressionConverter {}{0} / 2 - {1} / 2}">
    /// 				<Binding Path="ActualHeight" ElementName="_panel"/>
    /// 				<Binding Path="ActualHeight" ElementName="_border"/>
    /// 			</MultiBinding>
    /// 		</Popup.VerticalOffset>
    /// 
    /// 		<Border x:Name="_border" Background="White" BorderThickness="1">
    /// 			<Label>Here is the popup.</Label>
    /// 		</Border>
    /// 	</Popup>
    /// </StackPanel>
    /// ]]>
    /// </code>
    /// </example>
    public sealed class ExpressionConverterExtension : MarkupExtension
    {
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(ExpressionConverterExtension));
        private string _expression;

        /// <summary>
        /// Gets or sets the expression to use in the <see cref="ExpressionConverter"/>.
        /// </summary>
        [ConstructorArgument("expression")]
        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                _expression = value;
            }
        }

        /// <summary>
        /// Constructs a default instance of <c>ExpressionConverterExtension</c>.
        /// </summary>
        public ExpressionConverterExtension()
        {
        }

        /// <summary>
        /// Constructs an instance of <c>ExpressionConverterExtension</c> with the given expression.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        public ExpressionConverterExtension(string expression)
        {
            _expression = expression;
        }

        /// <summary>
        /// Provides an instance of <see cref="ExpressionConverter"/> based on <see cref="Expression"/>.
        /// </summary>
        /// <param name="serviceProvider">
        /// An object that can provide services.
        /// </param>
        /// <returns>
        /// The instance of <see cref="ExpressionConverter"/>.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            exceptionHelper.ResolveAndThrowIf(_expression == null, "NoExpression");
            return new ExpressionConverter(_expression);
        }
    }
#endif
}