using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using WithAngularApp.Server.Data.Models;

namespace WithAngularApp.Server.Data
{
	public class DataClient
	{
		public static List<CatType>? catTypeList;
		public static List<CatStat>? catStatList;
		public static List<string>? catNameList;

		public static void ParseData()
		{
			var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				HasHeaderRecord = false,
			};

			using (var reader = new StreamReader("Data/Tables/tblCatType.csv"))
			using (var csv = new CsvReader(reader, configuration))
			{
				var records = csv.GetRecords<CatType>();
				catTypeList = records.ToList();
			}

			using (var reader = new StreamReader("Data/Tables/tblCatStat.csv"))
			using (var csv = new CsvReader(reader, configuration))
			{
				var records = csv.GetRecords<CatStat>();
				catStatList = records.ToList();
			}

			var recordsList = new List<string>();
			using (var reader = new StreamReader("Data/Tables/tblCatName.csv"))
			{
				string? line;
				while ((line = reader.ReadLine()) != null)
				{
					if (!string.IsNullOrEmpty(line))
					{
						recordsList.Add(line);
					}
				}
			}
			catNameList = recordsList;
		}

		public static List<CatType> GetCatTypeList()
		{
			if (catTypeList == null)
				return new List<CatType> { };
			return catTypeList;
		}

		public static List<CatStat> GetCatStatList()
		{
			if (catStatList == null)
				return new List<CatStat> { };
			return catStatList;
		}

		public static List<string> GetCatNameList()
		{
			if (catNameList == null)
				return new List<string> { };
			return catNameList;
		}
	}
}
