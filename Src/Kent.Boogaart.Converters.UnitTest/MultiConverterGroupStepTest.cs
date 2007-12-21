using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using NUnit.Framework;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
	[TestFixture]
	public sealed class MultiConverterGroupStepTest : UnitTest
	{
		private MultiConverterGroupStep _multiConverterGroupStep;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_multiConverterGroupStep = new MultiConverterGroupStep();
		}

		[Test]
		public void Constructor_ShouldSetDefaults()
		{
			Assert.IsNotNull(_multiConverterGroupStep.Converters);
			Assert.AreEqual(0, _multiConverterGroupStep.Converters.Count);
		}

		[Test]
		public void Converters_ShouldAllowManipulationOfConverters()
		{
			IMultiValueConverter converter1 = Mocks.DynamicMock<IMultiValueConverter>();
			IMultiValueConverter converter2 = Mocks.DynamicMock<IMultiValueConverter>();
			IMultiValueConverter converter3 = Mocks.DynamicMock<IMultiValueConverter>();
			Mocks.ReplayAll();

			_multiConverterGroupStep.Converters.Add(converter1);
			_multiConverterGroupStep.Converters.Add(converter2);
			_multiConverterGroupStep.Converters.Add(converter3);

			Assert.AreEqual(3, _multiConverterGroupStep.Converters.Count);
			Assert.IsTrue(_multiConverterGroupStep.Converters.Contains(converter1));
			Assert.IsTrue(_multiConverterGroupStep.Converters.Contains(converter2));
			Assert.IsTrue(_multiConverterGroupStep.Converters.Contains(converter3));
		}
	}
}
