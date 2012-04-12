using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Kent.Boogaart.Converters.Examples.SL
{
    public partial class MainPage : UserControl
    {
        public static readonly DependencyProperty DateTimeUtcProperty = DependencyProperty.Register("DateTimeUtc",
            typeof(DateTime),
            typeof(MainPage),
            new PropertyMetadata(DateTime.MinValue));

        public DateTime DateTimeUtc
        {
            get
            {
                return (DateTime)GetValue(DateTimeUtcProperty);
            }
            set
            {
                SetValue(DateTimeUtcProperty, value);
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private void updateDateTime_Click(object sender, EventArgs e)
        {
            DateTimeUtc = DateTime.UtcNow;
        }
    }
}
