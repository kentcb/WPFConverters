using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using NUnit.Framework;
using Rhino.Mocks;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
	[TestFixture]
	public sealed class MultiConverterGroupTest : UnitTest
	{
		private MultiConverterGroup _multiConverterGroup;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_multiConverterGroup = new MultiConverterGroup();
		}

		[Test]
		public void Constructor_ShouldSetDefaults()
		{
			Assert.IsNotNull(_multiConverterGroup.Steps);
			Assert.AreEqual(0, _multiConverterGroup.Steps.Count);
		}

		[Test]
		public void Steps_ShouldAllowManipulationOfSteps()
		{
			MultiConverterGroupStep step1 = new MultiConverterGroupStep();
			MultiConverterGroupStep step2 = new MultiConverterGroupStep();
			MultiConverterGroupStep step3 = new MultiConverterGroupStep();

			_multiConverterGroup.Steps.Add(step1);
			_multiConverterGroup.Steps.Add(step2);
			_multiConverterGroup.Steps.Add(step3);

			Assert.AreEqual(3, _multiConverterGroup.Steps.Count);
			Assert.IsTrue(_multiConverterGroup.Steps.Contains(step1));
			Assert.IsTrue(_multiConverterGroup.Steps.Contains(step2));
			Assert.IsTrue(_multiConverterGroup.Steps.Contains(step3));
		}

		[Test]
		public void Convert_ShouldReturnUnsetValueIfNoSteps()
		{
			Assert.AreEqual(DependencyProperty.UnsetValue, _multiConverterGroup.Convert(new object[] { }, null, null, null));
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "The final step in a MultiConverterGroup must have a single converter added to it.")]
		public void Convert_ShouldThrowIfLastStepHasMoreThanOneConverterInIt()
		{
			MultiConverterGroupStep step1 = new MultiConverterGroupStep();
			MultiConverterGroupStep step2 = new MultiConverterGroupStep();

			step1.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			step1.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			step2.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			step2.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			Mocks.ReplayAll();

			_multiConverterGroup.Steps.Add(step1);
			_multiConverterGroup.Steps.Add(step2);

			_multiConverterGroup.Convert(new object[] { }, null, null, null);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Each step in a MultiConverterGroup must have at least one converter added to it.")]
		public void Convert_ShouldThrowIfAnyStepHasNoConvertersInIt()
		{
			MultiConverterGroupStep step1 = new MultiConverterGroupStep();
			MultiConverterGroupStep step2 = new MultiConverterGroupStep();
			MultiConverterGroupStep step3 = new MultiConverterGroupStep();

			step1.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			step1.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			step3.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			Mocks.ReplayAll();

			_multiConverterGroup.Steps.Add(step1);
			_multiConverterGroup.Steps.Add(step2);
			_multiConverterGroup.Steps.Add(step3);

			_multiConverterGroup.Convert(new object[] { }, null, null, null);
		}

		[Test]
		public void Convert_ShouldExecuteStepsInOrder()
		{
			object[] values = new object[] { "abc", 123 };
			Type targetType = typeof(int);
			object parameter = "parameter";
			CultureInfo cultureInfo = new CultureInfo("de-DE");

			IMultiValueConverter converter1 = Mocks.DynamicMock<IMultiValueConverter>();
			Expect.Call(converter1.Convert(values, targetType, parameter, cultureInfo)).Return("converted value1");
			IMultiValueConverter converter2 = Mocks.DynamicMock<IMultiValueConverter>();
			Expect.Call(converter2.Convert(values, targetType, parameter, cultureInfo)).Return("converted value2");
			IMultiValueConverter converter3 = Mocks.DynamicMock<IMultiValueConverter>();
			Expect.Call(converter3.Convert(new object[] { "converted value1", "converted value2" }, targetType, parameter, cultureInfo)).Return("final converted value");
			Mocks.ReplayAll();

			MultiConverterGroupStep step1 = new MultiConverterGroupStep();
			step1.Converters.Add(converter1);
			step1.Converters.Add(converter2);
			MultiConverterGroupStep step2 = new MultiConverterGroupStep();
			step2.Converters.Add(converter3);
			_multiConverterGroup.Steps.Add(step1);
			_multiConverterGroup.Steps.Add(step2);

			Assert.AreEqual("final converted value", _multiConverterGroup.Convert(values, targetType, parameter, cultureInfo));
		}

		[Test]
		public void ConvertBack_ShouldReturnNullIfNoSteps()
		{
			Assert.IsNull(_multiConverterGroup.ConvertBack(new object[] { }, null, null, null));
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "The final step in a MultiConverterGroup must have a single converter added to it.")]
		public void ConvertBack_ShouldThrowIfLastStepHasMoreThanOneConverterInIt()
		{
			MultiConverterGroupStep step1 = new MultiConverterGroupStep();
			MultiConverterGroupStep step2 = new MultiConverterGroupStep();

			step1.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			step1.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			step2.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			step2.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			Mocks.ReplayAll();

			_multiConverterGroup.Steps.Add(step1);
			_multiConverterGroup.Steps.Add(step2);

			_multiConverterGroup.ConvertBack(new object[] { }, null, null, null);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Each step in a MultiConverterGroup must have at least one converter added to it.")]
		public void ConvertBack_ShouldThrowIfAnyStepHasNoConvertersInIt()
		{
			MultiConverterGroupStep step1 = new MultiConverterGroupStep();
			MultiConverterGroupStep step2 = new MultiConverterGroupStep();
			MultiConverterGroupStep step3 = new MultiConverterGroupStep();

			step1.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			step1.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			step3.Converters.Add(Mocks.DynamicMock<IMultiValueConverter>());
			Mocks.ReplayAll();

			_multiConverterGroup.Steps.Add(step1);
			_multiConverterGroup.Steps.Add(step2);
			_multiConverterGroup.Steps.Add(step3);

			_multiConverterGroup.ConvertBack(new object[] { }, null, null, null);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage="Step with index 1 produced 5 values but the subsequent step (index 0) requires 2 values.")]
		public void ConvertBack_ShouldThrowIfNumberOfValuesProducedByStepDoesNotEqualNumberOfConvertersInNextStep()
		{
			object[] values = new object[] { "abc", 123 };
			Type[] targetTypes = new Type[] { };
			object parameter = "parameter";
			CultureInfo cultureInfo = new CultureInfo("de-DE");

			IMultiValueConverter converter3 = Mocks.DynamicMock<IMultiValueConverter>();
			Expect.Call(converter3.ConvertBack("final converted value", targetTypes, parameter, cultureInfo)).Repeat.Any().Return(new object[] { "converted value1", "converted value2", "too", "many", "values" });
			IMultiValueConverter converter1 = Mocks.DynamicMock<IMultiValueConverter>();
			Expect.Call(converter1.ConvertBack("converted value1", targetTypes, parameter, cultureInfo)).Repeat.Any().Return(values);
			IMultiValueConverter converter2 = Mocks.DynamicMock<IMultiValueConverter>();
			Expect.Call(converter2.ConvertBack("converted value2", targetTypes, parameter, cultureInfo)).Repeat.Never();
			Mocks.ReplayAll();

			MultiConverterGroupStep step1 = new MultiConverterGroupStep();
			step1.Converters.Add(converter1);
			step1.Converters.Add(converter2);
			MultiConverterGroupStep step2 = new MultiConverterGroupStep();
			step2.Converters.Add(converter3);
			_multiConverterGroup.Steps.Add(step1);
			_multiConverterGroup.Steps.Add(step2);

			_multiConverterGroup.ConvertBack("final converted value", targetTypes, parameter, cultureInfo);
		}

		[Test]
		public void ConvertBack_ShouldExecuteStepsInReverseOrder()
		{
			object[] values = new object[] { "abc", 123 };
			Type[] targetTypes = new Type[] { };
			object parameter = "parameter";
			CultureInfo cultureInfo = new CultureInfo("de-DE");

			IMultiValueConverter converter3 = Mocks.DynamicMock<IMultiValueConverter>();
			Expect.Call(converter3.ConvertBack("final converted value", targetTypes, parameter, cultureInfo)).Return(new object[] { "converted value1", "converted value2" });
			IMultiValueConverter converter1 = Mocks.DynamicMock<IMultiValueConverter>();
			Expect.Call(converter1.ConvertBack("converted value1", targetTypes, parameter, cultureInfo)).Return(values);
			IMultiValueConverter converter2 = Mocks.DynamicMock<IMultiValueConverter>();
			Expect.Call(converter2.ConvertBack("converted value2", targetTypes, parameter, cultureInfo)).Repeat.Never();
			Mocks.ReplayAll();

			MultiConverterGroupStep step1 = new MultiConverterGroupStep();
			step1.Converters.Add(converter1);
			step1.Converters.Add(converter2);
			MultiConverterGroupStep step2 = new MultiConverterGroupStep();
			step2.Converters.Add(converter3);
			_multiConverterGroup.Steps.Add(step1);
			_multiConverterGroup.Steps.Add(step2);

			Assert.AreEqual(values, _multiConverterGroup.ConvertBack("final converted value", targetTypes, parameter, cultureInfo));
		}
	}
}