# `FormatConverter`

The `FormatConverter` and `FormatConverterExtension` classes allow you to convert any number of `object` instances to a `string` by using .NET’s standard formatting capabilities. They accept a single parameter, `FormatString`, which defines the format for the resultant string. 

When binding to a single value it can be used as follows: 

```XML
<TextBox x:Name="_textBox"/> 
<Label Content="{Binding Text, ElementName=_textBox, Converter={con:FormatConverter {}Your name is {0}}}"/>
```

Note how the format string passed to the `FormatConverter` is escaped with `"{}"`, which is necessary because it contains curly brace characters within it. The format string on its own is simply `"Your name is {0}"`. 

Binding to multiple values looks like this: 

```XML
<TextBox x:Name="_textBox1"/> 
<TextBox x:Name="_textBox2"/> 
<Label> 
    <Label.Content> 
        <MultiBinding Converter="{con:FormatConverter {}You said {0} and {1}.}"> 
            <Binding Path="Text" ElementName="_textBox1"/> 
            <Binding Path="Text" ElementName="_textBox2"/> 
        </MultiBinding> 
    </Label.Content> 
</Label>
```