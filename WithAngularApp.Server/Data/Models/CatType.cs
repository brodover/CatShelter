using CsvHelper.Configuration.Attributes;

namespace WithAngularApp.Server.Data.Models
{
	public class CatType
	{
		[Index(0)]
		public int Id { get; set; }
		[Index(1)]
		public int Pattern { get; set; }
		[Index(2)]
		public int Color { get; set; }
	}
}
