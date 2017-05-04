# `ConverterGroup`

The `Binding` class only allows you to provide one converter. The `ConverterGroup` class allows you to construct a pipeline of converters that can be assigned to a `Binding`, which essentially annuls this restriction. Here’s an example: 

```XML 
TextBox x:Name="_textBox"/> 
<Label> 
    <Label.Content> 
        <Binding Path="Text" ElementName="_textBox"> 
            <Binding.Converter> 
                <con:ConverterGroup> 
                    <con:CaseConverter Casing="Upper"/> 
                    <con:FormatConverter FormatString="In uppercase, you entered ‘{0}’."/> 
                </con:ConverterGroup> 
            </Binding.Converter> 
        </Binding> 
    </Label.Content> 
</Label>
```

In this example, any input in the `TextBox` is first converted to uppercase with the `CaseConverter` and then formatted via the `FormatConverter`. If all converters in the pipeline support backward conversions (not so in this case) then the `ConverterGroup` will also support backwards conversions.