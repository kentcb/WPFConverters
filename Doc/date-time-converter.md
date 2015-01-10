# `DateTimeConverter`

The `DateTimeConverter` and `DateTimeConverterExtension` classes allow you to perform specialized conversions on `DateTime` instances during data binding. Using these classes, you can do any of the following: 

* Convert between different kinds of `DateTime` (e.g. convert between UTC and local time). 
* Convert between different kinds of `DateTime` without changing the underlying `DateTime` value (i.e. convert by using `DateTime.SpecifyKind()` instead of `ToLocalTime()` or `ToUniversalTime()`). 
* Adjust `DateTime` instances during conversion by adding a `TimeSpan` to them. 

The `DateTimeConverter` supports both forward and backwards conversions. This allows you, for example, to store `DateTime` instances in UTC format in your business objects but convert them to local time prior to using them in your interface. The following example shows how you could achieve this (and assumes the existence of a WPF `DatePicker` control): 

```XML
<DatePicker Value="{Binding StartDate, Converter={con:DateTimeConverter TargetKind=Local, SourceKind=Utc}}"/>
```