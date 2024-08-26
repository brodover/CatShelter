using CsvHelper.Configuration.Attributes;

namespace WithAngularApp.Server.Data.Models
{
	public class CatType
	{
		[Index(0)]
		public byte Id { get; set; }
		[Index(1)]
		public byte Pattern { get; set; }
		[Index(2)]
		public byte Color { get; set; }
	}
}
