using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Kent.Boogaart.Converters.Examples
{
	public partial class MainWindow : Window
	{
		public static readonly DependencyProperty DateTimeUtcProperty = DependencyProperty.Register("DateTimeUtc",
			typeof(DateTime),
			typeof(MainWindow));

		public DateTime DateTimeUtc
		{
			get
			{
				return (DateTime) GetValue(DateTimeUtcProperty);
			}
			set
			{
				SetValue(DateTimeUtcProperty, value);
			}
		}

		public MainWindow()
		{
			InitializeComponent();
			_comboBox1.ItemsSource = Enum.GetValues(typeof(UriFormat));
			_textBox7.Text = DateTime.Now.ToString("g");
			_genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
		}

		private void updateDateTime_Click(object sender, EventArgs e)
		{
			DateTimeUtc = DateTime.UtcNow;
		}
	}
}
