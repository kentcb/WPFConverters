using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using NUnit.Framework;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
	[TestFixture]
	public sealed class DateTimeConverterTest : UnitTest
	{
		private DateTimeConverter _dateTimeConverter;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_dateTimeConverter = new DateTimeConverter();
		}

		[Test]
		public void Constructor_ShouldSetDefaults()
		{
			Assert.AreEqual(DateTimeKind.Unspecified, _dateTimeConverter.SourceKind);
			Assert.AreEqual(DateTimeKind.Unspecified, _dateTimeConverter.TargetKind);
		}

		[Test]
		public void Constructor_SourceAndTargetKind_ShouldSetSourceAndTargetKind()
		{
			_dateTimeConverter = new DateTimeConverter(DateTimeKind.Local, DateTimeKind.Local);
			Assert.AreEqual(DateTimeKind.Local, _dateTimeConverter.SourceKind);
			Assert.AreEqual(DateTimeKind.Local, _dateTimeConverter.TargetKind);
			_dateTimeConverter = new DateTimeConverter(DateTimeKind.Utc, DateTimeKind.Unspecified);
			Assert.AreEqual(DateTimeKind.Utc, _dateTimeConverter.SourceKind);
			Assert.AreEqual(DateTimeKind.Unspecified, _dateTimeConverter.TargetKind);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), ExpectedMessage = "'100' is not a valid value for property 'SourceKind'.")]
		public void SourceKind_ShouldThrowIfInvalid()
		{
			_dateTimeConverter.SourceKind = (DateTimeKind) 100;
		}

		[Test]
		public void SourceKind_ShouldGetAndSetSourceKind()
		{
			Assert.AreEqual(DateTimeKind.Unspecified, _dateTimeConverter.SourceKind);
			_dateTimeConverter.SourceKind = DateTimeKind.Local;
			Assert.AreEqual(DateTimeKind.Local, _dateTimeConverter.SourceKind);
			_dateTimeConverter.SourceKind = DateTimeKind.Utc;
			Assert.AreEqual(DateTimeKind.Utc, _dateTimeConverter.SourceKind);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), ExpectedMessage = "'100' is not a valid value for property 'TargetKind'.")]
		public void TargetKind_ShouldThrowIfInvalid()
		{
			_dateTimeConverter.TargetKind = (DateTimeKind) 100;
		}

		[Test]
		public void TargetKind_ShouldGetAndSetTargetKind()
		{
			Assert.AreEqual(DateTimeKind.Unspecified, _dateTimeConverter.TargetKind);
			_dateTimeConverter.TargetKind = DateTimeKind.Local;
			Assert.AreEqual(DateTimeKind.Local, _dateTimeConverter.TargetKind);
			_dateTimeConverter.TargetKind = DateTimeKind.Utc;
			Assert.AreEqual(DateTimeKind.Utc, _dateTimeConverter.TargetKind);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), ExpectedMessage = "'100' is not a valid value for property 'ConversionMode'.")]
		public void ConversionMode_ShouldThrowIfInvalid()
		{
			_dateTimeConverter.ConversionMode = (DateTimeConversionMode) 100;
		}

		[Test]
		public void ConversionMode_ShouldGetAndSetConversionMode()
		{
			Assert.AreEqual(DateTimeConversionMode.DoConversion, _dateTimeConverter.ConversionMode);
			_dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
			Assert.AreEqual(DateTimeConversionMode.SpecifyKindOnly, _dateTimeConverter.ConversionMode);
		}

		[Test]
		public void SourceAdjustment_ShouldGetAndSetSourceAdjustment()
		{
			Assert.AreEqual(TimeSpan.Zero, _dateTimeConverter.SourceAdjustment);
			_dateTimeConverter.SourceAdjustment = TimeSpan.FromDays(2);
			Assert.AreEqual(TimeSpan.FromDays(2), _dateTimeConverter.SourceAdjustment);
		}

		[Test]
		public void TargetAdjustment_ShouldGetAndSetTargetAdjustment()
		{
			Assert.AreEqual(TimeSpan.Zero, _dateTimeConverter.TargetAdjustment);
			_dateTimeConverter.TargetAdjustment = TimeSpan.FromDays(2);
			Assert.AreEqual(TimeSpan.FromDays(2), _dateTimeConverter.TargetAdjustment);
		}

		[Test]
		public void Convert_ShouldReturnUnsetValueIfValueIsNotADateTime()
		{
			Assert.AreSame(DependencyProperty.UnsetValue, _dateTimeConverter.Convert(null, null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _dateTimeConverter.Convert("abc", null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _dateTimeConverter.Convert(123, null, null, null));
		}

		[Test]
		public void Convert_ShouldNotConvertIfTargetKindIsUnspecified()
		{
			Assert.AreEqual(DateTimeKind.Unspecified, _dateTimeConverter.TargetKind);
			DateTime sourceDateTime = DateTime.UtcNow;
			Assert.AreEqual(DateTimeKind.Utc, ((DateTime) _dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);
			Assert.AreEqual(sourceDateTime, _dateTimeConverter.Convert(sourceDateTime, null, null, null));
		}

		[Test]
		public void Convert_ShouldConvertDateTimesToTargetKind()
		{
			DateTime sourceDateTime = DateTime.UtcNow;
			_dateTimeConverter.TargetKind = DateTimeKind.Local;
			Assert.AreEqual(DateTimeKind.Local, ((DateTime) _dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);

			_dateTimeConverter.TargetKind = DateTimeKind.Utc;
			Assert.AreEqual(DateTimeKind.Utc, ((DateTime) _dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);
			Assert.AreEqual(sourceDateTime, _dateTimeConverter.Convert(sourceDateTime, null, null, null));

			sourceDateTime = new DateTime(Environment.TickCount, DateTimeKind.Unspecified);
			Assert.AreEqual(DateTimeKind.Utc, ((DateTime) _dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);
		}

		[Test]
		public void Convert_ShouldNotAdjustValueIsConversionModeIfSpecifyKindOnly()
		{
			DateTime sourceDateTime = DateTime.UtcNow;
			_dateTimeConverter.TargetKind = DateTimeKind.Local;
			_dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
			Assert.AreEqual(DateTimeKind.Local, ((DateTime) _dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);
			Assert.AreEqual(sourceDateTime, _dateTimeConverter.Convert(sourceDateTime, null, null, null));
		}

		[Test]
		public void Convert_ShouldApplyTargetAdjustment()
		{
			DateTime sourceDateTime = DateTime.UtcNow;
			_dateTimeConverter.TargetAdjustment = TimeSpan.FromMinutes(1);
			_dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;

			Assert.AreEqual(sourceDateTime.Add(TimeSpan.FromMinutes(1)), _dateTimeConverter.Convert(sourceDateTime, null, null, null));
		}

		[Test]
		public void ConvertBack_ShouldReturnUnsetValueIfValueIsNotADateTime()
		{
			Assert.AreSame(DependencyProperty.UnsetValue, _dateTimeConverter.ConvertBack(null, null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _dateTimeConverter.ConvertBack("abc", null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _dateTimeConverter.ConvertBack(123, null, null, null));
		}

		[Test]
		public void ConvertBack_ShouldNotConvertIfSourceKindIsUnspecified()
		{
			Assert.AreEqual(DateTimeKind.Unspecified, _dateTimeConverter.SourceKind);
			DateTime targetDateTime = DateTime.UtcNow;
			Assert.AreEqual(DateTimeKind.Utc, ((DateTime) _dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);
			Assert.AreEqual(targetDateTime, _dateTimeConverter.ConvertBack(targetDateTime, null, null, null));
		}

		[Test]
		public void ConvertBack_ShouldConvertDateTimesToSourceKind()
		{
			DateTime targetDateTime = DateTime.UtcNow;
			_dateTimeConverter.SourceKind = DateTimeKind.Local;
			Assert.AreEqual(DateTimeKind.Local, ((DateTime) _dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);

			_dateTimeConverter.SourceKind = DateTimeKind.Utc;
			Assert.AreEqual(DateTimeKind.Utc, ((DateTime) _dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);
			Assert.AreEqual(targetDateTime, _dateTimeConverter.ConvertBack(targetDateTime, null, null, null));

			targetDateTime = new DateTime(Environment.TickCount, DateTimeKind.Unspecified);
			Assert.AreEqual(DateTimeKind.Utc, ((DateTime) _dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);
		}

		[Test]
		public void ConvertBack_ShouldNotAdjustValueIsConversionModeIfSpecifyKindOnly()
		{
			DateTime targetDateTime = DateTime.UtcNow;
			_dateTimeConverter.SourceKind = DateTimeKind.Local;
			_dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
			Assert.AreEqual(DateTimeKind.Local, ((DateTime) _dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);
			Assert.AreEqual(targetDateTime, _dateTimeConverter.ConvertBack(targetDateTime, null, null, null));
		}

		[Test]
		public void ConvertBack_ShouldApplySourceAdjustment()
		{
			DateTime targetDateTime = DateTime.UtcNow;
			_dateTimeConverter.SourceAdjustment = TimeSpan.FromMinutes(1);
			_dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;

			Assert.AreEqual(targetDateTime.Add(TimeSpan.FromMinutes(1)), _dateTimeConverter.ConvertBack(targetDateTime, null, null, null));
		}
	}
}