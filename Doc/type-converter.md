# `TypeConverter`

The `TypeConverter` and `TypeConverterExtension` classes provide the ability to convert input values to different types. It is essentially a public implementation of the internal `DefaultValueConverter` and `SystemConvertConverter` BCL types. It is useful in coercing a value to required type during a pipeline conversion using `ConverterGroup`. Using it is straightforward: 

```XML
<Label> 
    <Label.Content> 
        <Binding Path="Dob"> 
            <Binding.Converter> 
                <con:ConverterGroup> 
                    <con:TypeConverter TargetType="{x:Type sys:DateTime}"/> 
                    <con:DateTimeConverter TargetKind="Local"/> 
                </con:ConverterGroup> 
            </Binding.Converter> 
        </Binding> 
    </Label.Content> 
</Label>
```

In this highly fabricated example, there is a `Dob` property that is a string when it really should be a `DateTime`. A `TypeConverter` is used to convert the string to a `DateTime` prior to feeding it into a `DateTimeConverter`.

The `TypeConverter` will first attempt to convert data by way of an `IConvertible` implementation. If the value does not implement `IConvertible` then an attempt will be made to use any `System.ComponentModel.TypeConverter` implementation type for the class. If all attempts to convert the value fail, `DependencyProperty.UnsetValue` will be returned.