using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// this is used to version artifacts. AssemblyInformationalVersion should use semantic versioning (http://semver.org/)
[assembly: AssemblyInformationalVersion("1.3.0")]
[assembly: AssemblyVersion("1.3.0.0")]

[assembly: AssemblyCompany("Kent Boogaart")]
[assembly: AssemblyProduct("WPF Converters")]
[assembly: AssemblyCopyright("© Copyright. Kent Boogaart.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: NeutralResourcesLanguage("en-US")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif