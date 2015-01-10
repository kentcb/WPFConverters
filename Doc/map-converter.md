# `MapConverter`

The `MapConverter` class (which has no corresponding markup extension) can be used to map one set of values to another. This is an extremely useful converter that has many use cases. Here are some examples: 

* Converting enumeration values to human-readable strings prior to display. 
* Converting between `bool` values and an `ImageSource` so the user sees a tick or a cross depending on the `bool` value. 

The `MapConverter` works by using a collection of `Mapping` objects. Each `Mapping` object specifies a value to map from and a value to map to. The same `Mapping` objects are used both for forward conversions and backward conversions. 

If no relevant `Mapping` object can be found during a conversion, the `MapConverter` uses its `FallbackBehavior` property to decide what to do. You can tell it to either return `DependencyProperty.UnsetValue`, return the original value it was asked to convert, or to return a fallback value specified by the `MapConverter.FallbackValue` property. 

An example should make this all clear: 

```XML
<Label> 
    <Label.Content> 
        <Binding Path="Gender"> 
            <Binding.Converter> 
                <con:MapConverter> 
                    <con:Mapping From="{x:Static Gender.Male}" To="Guy"/> 
                    <con:Mapping From="{x:Static Gender.Female}" To="Gal"/> 
                </con:MapConverter> 
            </Binding.Converter> 
        </Binding> 
    </Label.Content> 
</Label>
```

This example uses a `MapConverter` to convert from members in a `Gender` enumeration to either `"Guy"` or `"Gal"`. Now suppose that the `Gender` enumeration also defines a value of `Unknown`. The above mapping won’t successfully convert values of `Unknown` – it will just return `DependencyProperty.UnsetValue` instead. 

If you want unknown genders to display as `"Unknown"` you can either add another `Mapping` or just tell the `MapConverter` to return the original value if it cannot map the value it is given: 

```XML
<con:MapConverter FallbackBehavior="ReturnOriginalValue"> 
    <con:Mapping From="{x:Static Gender.Male}" To="Guy"/> 
    <con:Mapping From="{x:Static Gender.Female}" To="Gal"/> 
</con:MapConverter>
```

A `FallbackBehavior` of `ReturnOriginalValue` is extremely useful where you only want to map a subset of the total possible values.  A `FallbackBehavior` of `ReturnFallbackValue` is also very useful, but it allows you to specify the exact value to return if no mapping exists: 

```XML
<con:MapConverter FallbackBehavior="ReturnFallbackValue" FallbackValue="Not specified"> 
    <con:Mapping From="{x:Static Gender.Male}" To="Guy"/> 
    <con:Mapping From="{x:Static Gender.Female}" To="Gal"/> 
</con:MapConverter>
```

In this example, any gender with value `Unknown` will be converted to `"Not specified"`.