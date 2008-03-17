using System;
using NUnit.Framework;
using Kent.Boogaart.Converters.Markup;

namespace Kent.Boogaart.Converters.UnitTest.Markup
{
	[TestFixture]
	public sealed class DateTimeConverterExtensionTest : UnitTest
	{
		private DateTimeConverterExtension _dateTimeConverterExtension;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_dateTimeConverterExtension = new DateTimeConverterExtension();
		}

		[Test]
		public void Constructor_ShouldSetDefaults()
		{
			Assert.AreEqual(DateTimeKind.Unspecified, _dateTimeConverterExtension.SourceKind);
			Assert.AreEqual(DateTimeKind.Unspecified, _dateTimeConverterExtension.TargetKind);
			Assert.AreEqual(DateTimeConversionMode.DoConversion, _dateTimeConverterExtension.ConversionMode);
			Assert.AreEqual(TimeSpan.Zero, _dateTimeConverterExtension.SourceAdjustment);
			Assert.AreEqual(TimeSpan.Zero, _dateTimeConverterExtension.TargetAdjustment);
		}

		[Test]
		public void Constructor_Kinds_ShouldSetKinds()
		{
			_dateTimeConverterExtension = new DateTimeConverterExtension(DateTimeKind.Utc, DateTimeKind.Local);
			Assert.AreEqual(DateTimeKind.Utc, _dateTimeConverterExtension.SourceKind);
			Assert.AreEqual(DateTimeKind.Local, _dateTimeConverterExtension.TargetKind);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), ExpectedMessage = "Enum value '100' is not defined for enumeration 'System.DateTimeKind'.\r\nParameter name: value")]
		public void SourceKind_ShouldThrowIfInvalid()
		{
			_dateTimeConverterExtension.SourceKind = (DateTimeKind) 100;
		}

		[Test]
		public void SourceKind_ShouldGetAndSet()
		{
			Assert.AreEqual(DateTimeKind.Unspecified, _dateTimeConverterExtension.SourceKind);
			_dateTimeConverterExtension.SourceKind = DateTimeKind.Local;
			Assert.AreEqual(DateTimeKind.Local, _dateTimeConverterExtension.SourceKind);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), ExpectedMessage = "Enum value '100' is not defined for enumeration 'System.DateTimeKind'.\r\nParameter name: value")]
		public void TargetKind_ShouldThrowIfInvalid()
		{
			_dateTimeConverterExtension.TargetKind = (DateTimeKind) 100;
		}

		[Test]
		public void TargetKind_ShouldGetAndSet()
		{
			Assert.AreEqual(DateTimeKind.Unspecified, _dateTimeConverterExtension.TargetKind);
			_dateTimeConverterExtension.TargetKind = DateTimeKind.Local;
			Assert.AreEqual(DateTimeKind.Local, _dateTimeConverterExtension.TargetKind);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), ExpectedMessage = "Enum value '100' is not defined for enumeration 'Kent.Boogaart.Converters.DateTimeConversionMode'.\r\nParameter name: value")]
		public void ConversionMode_ShouldThrowIfInvalid()
		{
			_dateTimeConverterExtension.ConversionMode = (DateTimeConversionMode) 100;
		}

		[Test]
		public void ConversionMode_ShouldGetAndSet()
		{
			Assert.AreEqual(DateTimeConversionMode.DoConversion, _dateTimeConverterExtension.ConversionMode);
			_dateTimeConverterExtension.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
			Assert.AreEqual(DateTimeConversionMode.SpecifyKindOnly, _dateTimeConverterExtension.ConversionMode);
		}

		[Test]
		public void SourceAdjustment_ShouldGetAndSet()
		{
			Assert.AreEqual(TimeSpan.Zero, _dateTimeConverterExtension.SourceAdjustment);
			_dateTimeConverterExtension.SourceAdjustment = TimeSpan.FromDays(1);
			Assert.AreEqual(TimeSpan.FromDays(1), _dateTimeConverterExtension.SourceAdjustment);
		}

		[Test]
		public void TargetAdjustment_ShouldGetAndSet()
		{
			Assert.AreEqual(TimeSpan.Zero, _dateTimeConverterExtension.TargetAdjustment);
			_dateTimeConverterExtension.TargetAdjustment = TimeSpan.FromDays(1);
			Assert.AreEqual(TimeSpan.FromDays(1), _dateTimeConverterExtension.TargetAdjustment);
		}

		[Test]
		public void ProvideValue_ShouldYieldDateTimeConverterWithGivenInfo()
		{
			_dateTimeConverterExtension.SourceKind = DateTimeKind.Utc;
			_dateTimeConverterExtension.TargetKind = DateTimeKind.Local;
			_dateTimeConverterExtension.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
			_dateTimeConverterExtension.SourceAdjustment = TimeSpan.FromSeconds(3);
			_dateTimeConverterExtension.TargetAdjustment = TimeSpan.FromSeconds(-3);

			DateTimeConverter dateTimeConverter = _dateTimeConverterExtension.ProvideValue(null) as DateTimeConverter;
			Assert.IsNotNull(dateTimeConverter);
			Assert.AreEqual(DateTimeKind.Utc, dateTimeConverter.SourceKind);
			Assert.AreEqual(DateTimeKind.Local, dateTimeConverter.TargetKind);
			Assert.AreEqual(DateTimeConversionMode.SpecifyKindOnly, dateTimeConverter.ConversionMode);
			Assert.AreEqual(TimeSpan.FromSeconds(3), dateTimeConverter.SourceAdjustment);
			Assert.AreEqual(TimeSpan.FromSeconds(-3), dateTimeConverter.TargetAdjustment);
		}
	}
}
