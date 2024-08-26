using CsvHelper.Configuration.Attributes;

namespace WithAngularApp.Server.Data.Models
{
	public class CatStat
	{
		[Index(0)]
		public int Id { get; set; }
		[Index(1)]
		public byte Stat { get; set; }
		[Index(2)]
		public double Prob { get; set; }
	}
}
