# `ExpressionConverter`

The `ExpressionConverter` and `ExpressionConverterExtension` classes allow you to convert one or more bound values by running them through a C#-like expression. The expression uses placeholders of the form `{x}` to demarcate bound arguments, where `x` is the argument number starting at zero. 

Let’s start with something simple: 

```XML
<Canvas> 
    <Slider x:Name="_slider" Width="300" Minimum="1" Maximum="100"/> 
    <Rectangle Canvas.Top="20" Fill="Red" Width="100" Height="{Binding Value, ElementName=_slider}"/> 
    <Rectangle Canvas.Top="100" Fill="Blue" Width="100" Height="{Binding Value, ElementName=_slider, Converter={con:ExpressionConverter {}{0} * 2}}"/> 
</Canvas>
```

In this example, the value of the `Slider` dictates the height of the first rectangle. It also dictates the height of the second rectangle, but an `ExpressionConverter` is used to double the value first. 

Notice how the expression passed to the `ExpressionConverter` is escaped with `"{}"`. This is necessary because it includes curly brace characters. The expression on its own is simply `"{0} * 2"`. In English, this means "multiply the first argument by two". 

Now, let’s do something a little more complex: 

```XML
<StackPanel x:Name="_panel"> 
    <Label>What bands do you like?</Label> 
    <CheckBox x:Name="_rammstein">Rammstein</Label> 
    <CheckBox x:Name="_powderfinger">Powderfinger</Label> 
    <CheckBox x:Name="_nickelback">Nickelback</Label> 
    <CheckBox x:Name="_ministry">Ministry</Label> 
    <Button Content="Let me in!"> 
        <Button.IsEnabled> 
            <MultiBinding Converter="{ExpressionConverter {}{0} &amp;&amp; {1} &amp;&amp; {3} &amp;&amp; !{2}}"> 
                <Binding Path="IsChecked" ElementName="_rammstein"/> 
                <Binding Path="IsChecked" ElementName="_powderfinger"/> 
                <Binding Path="IsChecked" ElementName="_nickelback"/> 
                <Binding Path="IsChecked" ElementName="_ministry"/> 
            </MultiBinding> 
        </Button.IsEnabled> 
    </Button> 
</StackPanel>
```

In this example, the entry button is only enabled if the correct combination of bands is chosen. If you don’t like one of the good bands or if you do like the sucky band, you won’t be allowed in. 

The actual expression has again been escaped due to its presence in XAML. Unescaped, it is simply `"{0} && {1} && {3} && !{2}"`. An alternative approach that avoids all this escaping is to use a more long-winded syntax in the `MultiBinding` as follows: 

```XML
<MultiBinding> 
    <MultiBinding.Converter> 
        <con:ExpressionConverterExtension> 
            <con:ExpressionConverterExtension.Expression>
                &lt;![CDATA[
                {0} && {1} && {3} && !{2}
                ]]&gt;
            </con:ExpressionConverterExtension.Expression> 
        </con:ExpressionConverterExtension> 
    </MultiBinding.Converter> 
</MultiBinding>
```

The `ExpressionConverter` supports a lot of C# operators and we’ve only scratched the surface of the possibilities here. Please see the API documentation for a full list of supported operators.