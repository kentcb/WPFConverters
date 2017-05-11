# `BooleanToVisibilityConverter`

The `BooleanToVisibilityConverter` and `BooleanToVisibilityConverterExtension` classes allow you to convert `boolean` values to `Visibility` values, and vice-versa: 

```XML
<CheckBox x:Name="_checkBox" IsChecked="True">Uncheck to collapse text box</CheckBox> 
<TextBox Visibility="{Binding IsChecked, ElementName=_checkBox, Converter={con:BooleanToVisibilityConverter}}"/>
```

Unlike WPF's built-in converter of the same name, this converter has extra options to greatly improve its flexibility. Firstly, you can specify that `Visibility.Hidden` be used instead of `Visibility.Collapsed` by setting the `UseHidden` property to `true`. Secondly, you can reverse the return values by setting the `IsReversed` property to `true`.

When you have more source binding in a control's `Visibility` property:

```XML
<StackPanel x:Name="_panel"> 
    <Label>What bands do you like?</Label> 
    <CheckBox x:Name="_rammstein">Rammstein</Label> 
    <CheckBox x:Name="_powderfinger">Powderfinger</Label> 
    <CheckBox x:Name="_nickelback">Nickelback</Label> 
    <CheckBox x:Name="_ministry">Ministry</Label> 
    <Button Content="Let me in!"> 
        <Button.Visibility> 
            <MultiBinding Converter="{ExpressionConverter {}{0} &amp;&amp; {1} &amp;&amp; {3} &amp;&amp; !{2}}"> 
                <MultiBinding.Converter>
                    <con:MultiConverterGroup>
                        <con:MultiConverterGroupStep>
                            <con:ExpressionConverter>
                                <con:ExpressionConverter.Expression>
                                    <![CDATA[ {0} && {1} && {2} && {3}]]>
                                </con:ExpressionConverter.Expression>
                            </con:ExpressionConverter>
                        </con:MultiConverterGroupStep>
                        <con:MultiConverterGroupStep>
                            <con:BooleanToVisibilityConverter UseHidden="False"></con:BooleanToVisibilityConverter>
                        </con:MultiConverterGroupStep>
                    </con:MultiConverterGroup>
                </MultiBinding.Converter>
                <Binding Path="IsChecked" ElementName="_rammstein"/> 
                <Binding Path="IsChecked" ElementName="_powderfinger"/> 
                <Binding Path="IsChecked" ElementName="_nickelback"/> 
                <Binding Path="IsChecked" ElementName="_ministry"/> 
            </MultiBinding> 
        </Button.IsEnabled>
    </Button> 
</StackPanel>
```

In the example, the entry button is only show if the correct combination of bands is chosen. If you don’t like one of the good bands or if you do like the sucky band, you won’t be allowed in. 