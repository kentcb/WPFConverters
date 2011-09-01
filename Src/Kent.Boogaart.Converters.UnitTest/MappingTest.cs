using Xunit;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
	public sealed class MappingTest : UnitTest
	{
		private Mapping _mapping;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_mapping = new Mapping();
		}

		[Fact]
		public void Constructor_ShouldSetUpDefaults()
		{
			Assert.Null(_mapping.From);
			Assert.Null(_mapping.To);
		}

		[Fact]
		public void Constructor_FromTo_ShouldAssignToFromAndTo()
		{
			object from = new object();
			object to = new object();
			_mapping = new Mapping(from, to);
			Assert.Same(from, _mapping.From);
			Assert.Same(to, _mapping.To);
		}

		[Fact]
		public void From_ShouldAssignToFrom()
		{
			object from = new object();
			_mapping.From = from;
			Assert.Same(from, _mapping.From);
			_mapping.From = null;
			Assert.Null(_mapping.From);
		}

		[Fact]
		public void To_ShouldAssignToTo()
		{
			object to = new object();
			_mapping.To = to;
			Assert.Same(to, _mapping.To);
			_mapping.To = null;
			Assert.Null(_mapping.To);
		}
	}
}
