# WPF Converters

## What?

This project contains a set of generic binding converters that can be used in most any WPF (or Silverlight) application.

## Why?

Just about every non-trivial WPF application requires some custom binding converters. A set of dependable, general-purpose converters greatly reduces the number of converters that you must write yourself.

## Where?

The easiest way to get *WPF Converters* is to install via NuGet:

```
Install-Package Kent.Boogaart.Converters
```

## How?

```XML
<TextBox x:Name="nameTextBox"/>
<TextBlock>
    <Run>Your name in upper case is </Run>
    <Run Text="{Binding Text, ElementName=amountTextBox, Converter={con:CaseConverter Upper}}"/>
</TextBlock>
```

Please see [the documentation](Doc/overview.md) for more details. The source code download also includes sample projects for both WPF and Silverlight.

## Who?

*WPF Converters* is currently developed solely by [Kent Boogaart](http://kent-boogaart.com/).

## Primary Features

* WPF 3.5/4.0/4.5 support
* Silverlight 4.0 and 5.0 support
* Extensively unit tested
* Many converters including `ExpressionConverter`, which allows arbitrary C# expressions to be applied to bound values, and `MapConverter`, which maps input values to corresponding outputs.