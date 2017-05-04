# `CaseConverter`

The `CaseConverter` and `CaseConverterExtension` classes allow you to convert a string to upper or lower case. They accept a single parameter, `Casing`, which is used to specify the desired case for the resultant string. It is very simple to use, as the following example demonstrates: 

```XML
<TextBox x:Name="_textBox"/> 
<Label Content="{Binding Text, ElementName=_textBox, Converter={con:CaseConverter Upper}}"/> 
<Label Content="{Binding Text, ElementName=_textBox, Converter={con:CaseConverter Lower}}"/>
```

Anything typed in the `TextBox` will be displayed in uppercase in the first Label, and lowercase in the second.