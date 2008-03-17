using System;
using System.Windows;
using NUnit.Framework;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
	[TestFixture]
	public sealed class MapConverterTest : UnitTest
	{
		private MapConverter _mapConverter;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_mapConverter = new MapConverter();
		}

		[Test]
		public void Constructor_ShouldSetUpDefaults()
		{
			Assert.IsNotNull(_mapConverter.Mappings);
			Assert.IsEmpty(_mapConverter.Mappings as System.Collections.ICollection);
			Assert.AreEqual(FallbackBehavior.ReturnUnsetValue, _mapConverter.FallbackBehavior);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), ExpectedMessage="'100' is not a valid value for property 'FallbackBehavior'.")]
		public void FallbackBehavior_ShouldThrowIfInvalid()
		{
			_mapConverter.FallbackBehavior = (FallbackBehavior) 100;
		}

		[Test]
		public void FallbackBehavior_ShouldGetAndSetFallbackBehavior()
		{
			_mapConverter.FallbackBehavior = FallbackBehavior.ReturnOriginalValue;
			Assert.AreEqual(FallbackBehavior.ReturnOriginalValue, _mapConverter.FallbackBehavior);
			_mapConverter.FallbackBehavior = FallbackBehavior.ReturnUnsetValue;
			Assert.AreEqual(FallbackBehavior.ReturnUnsetValue, _mapConverter.FallbackBehavior);
		}

		[Test]
		public void Mappings_ShouldAllowManipulation()
		{
			Mapping mapping = new Mapping("from", "to");
			_mapConverter.Mappings.Add(mapping);
			Assert.IsTrue(_mapConverter.Mappings.Contains(mapping));
		}

		[Test]
		public void Convert_ShouldHonourFallbackBehaviorIfConversionFails()
		{
			_mapConverter.Mappings.Add(new Mapping("from", "to"));

			Assert.AreSame(DependencyProperty.UnsetValue, _mapConverter.Convert(null, null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _mapConverter.Convert("abc", null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _mapConverter.Convert(123, null, null, null));

			_mapConverter.FallbackBehavior = FallbackBehavior.ReturnOriginalValue;

			Assert.IsNull(_mapConverter.Convert(null, null, null, null));
			Assert.AreSame("abc", _mapConverter.Convert("abc", null, null, null));
			Assert.AreEqual(123, _mapConverter.Convert(123, null, null, null));
		}

		[Test]
		public void Convert_ShouldReturnToValueIfMappingExists()
		{
			_mapConverter.Mappings.Add(new Mapping("from", "to"));
			_mapConverter.Mappings.Add(new Mapping(null, "NULL"));
			_mapConverter.Mappings.Add(new Mapping(123, 123.5d));

			Assert.AreSame("to", _mapConverter.Convert("from", null, null, null));
			Assert.AreSame("NULL", _mapConverter.Convert(null, null, null, null));
			Assert.AreEqual(123.5d, _mapConverter.Convert(123, null, null, null));
		}

		[Test]
		public void ConvertBack_ShouldReturnDefaultValueIfNoMappingExists()
		{
			_mapConverter.Mappings.Add(new Mapping("from", "to"));

			Assert.AreSame(DependencyProperty.UnsetValue, _mapConverter.ConvertBack(null, null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _mapConverter.ConvertBack("abc", null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _mapConverter.ConvertBack(123, null, null, null));

			_mapConverter.FallbackBehavior = FallbackBehavior.ReturnOriginalValue;

			Assert.IsNull(_mapConverter.ConvertBack(null, null, null, null));
			Assert.AreSame("abc", _mapConverter.ConvertBack("abc", null, null, null));
			Assert.AreEqual(123, _mapConverter.ConvertBack(123, null, null, null));
		}

		[Test]
		public void ConvertBack_ShouldReturnToValueIfMappingExists()
		{
			_mapConverter.Mappings.Add(new Mapping("from", "to"));
			_mapConverter.Mappings.Add(new Mapping(null, "NULL"));
			_mapConverter.Mappings.Add(new Mapping(123, 123.5d));

			Assert.AreSame("from", _mapConverter.ConvertBack("to", null, null, null));
			Assert.IsNull(_mapConverter.ConvertBack("NULL", null, null, null));
			Assert.AreEqual(123, _mapConverter.ConvertBack(123.5d, null, null, null));
		}
	}
}
