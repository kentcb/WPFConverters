using System;
using System.Windows;

namespace Kent.Boogaart.Converters.Examples.SL
{
    public sealed class ValueProvider
    {
        public Visibility Visible
        {
            get { return Visibility.Visible; }
        }

        public UriFormat SafeUnescaped
        {
            get { return UriFormat.SafeUnescaped; }
        }

        public UriFormat UriEscaped
        {
            get { return UriFormat.UriEscaped; }
        }
    }
}