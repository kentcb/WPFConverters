using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

[assembly: AssemblyTitle("WPF Converters")]
[assembly: AssemblyDescription("A set of WPF converters applicable to almost any WPF venture.")]
[assembly: InternalsVisibleTo("Kent.Boogaart.Converters.UnitTest, PublicKey=00240000048000009400000006020000002400005253413100040000010001004B113F0FD5F5931D7D1B3980BC019600E10F81CAB363C87835D8D055263916CECDA3FF094746B14509F5A300A11F19C92DD6154D2900C2460121C64BE915C26AEDA2E140FB623F2B97742F5C2042047EFD969B1455C3095BFE129B7E1C4F8EBF8A32CC0462686464169389332285347C5FBC4575BCF44FD6084AB487FB7EBCDF")]
[assembly: XmlnsDefinition("http://schemas.kent.boogaart.com/converters", "Kent.Boogaart.Converters")]

#if !SILVERLIGHT40
[assembly: XmlnsDefinition("http://schemas.kent.boogaart.com/converters", "Kent.Boogaart.Converters.Markup")]
#endif