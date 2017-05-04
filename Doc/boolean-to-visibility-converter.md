# `BooleanToVisibilityConverter`

The `BooleanToVisibilityConverter` and `BooleanToVisibilityConverterExtension` classes allow you to convert `boolean` values to `Visibility` values, and vice-versa: 

```XML
<CheckBox x:Name="_checkBox" IsChecked="True">Uncheck to collapse text box</CheckBox> 
<TextBox Visibility="{Binding IsChecked, ElementName=_checkBox, Converter={con:BooleanToVisibilityConverter}}"/>
```

Unlike WPF's built-in converter of the same name, this converter has extra options to greatly improve its flexibility. Firstly, you can specify that `Visibility.Hidden` be used instead of `Visibility.Collapsed` by setting the `UseHidden` property to `true`. Secondly, you can reverse the return values by setting the `IsReversed` property to `true`.