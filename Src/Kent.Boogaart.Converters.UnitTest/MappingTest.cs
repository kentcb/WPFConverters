using NUnit.Framework;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
	[TestFixture]
	public sealed class MappingTest : UnitTest
	{
		private Mapping _mapping;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_mapping = new Mapping();
		}

		[Test]
		public void Constructor_ShouldSetUpDefaults()
		{
			Assert.IsNull(_mapping.From);
			Assert.IsNull(_mapping.To);
		}

		[Test]
		public void Constructor_FromTo_ShouldAssignToFromAndTo()
		{
			object from = new object();
			object to = new object();
			_mapping = new Mapping(from, to);
			Assert.AreSame(from, _mapping.From);
			Assert.AreSame(to, _mapping.To);
		}

		[Test]
		public void From_ShouldAssignToFrom()
		{
			object from = new object();
			_mapping.From = from;
			Assert.AreSame(from, _mapping.From);
			_mapping.From = null;
			Assert.IsNull(_mapping.From);
		}

		[Test]
		public void To_ShouldAssignToTo()
		{
			object to = new object();
			_mapping.To = to;
			Assert.AreSame(to, _mapping.To);
			_mapping.To = null;
			Assert.IsNull(_mapping.To);
		}
	}
}
