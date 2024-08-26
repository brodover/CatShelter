using WithAngularApp.Server.Database.Models;

namespace WithAngularApp.Server.Data.Models
{
	public class Request
	{
		public class Owner
		{
			public class GetOwnerByProviderId
			{
				public string ProviderId { get; set; }
				public byte Provider { get; set; }
			}
			public class LinkProvider
			{
				public string Id { get; set; }
				public string ProviderId { get; set; }
				public byte Provider { get; set; }
			}
		}

		public class Cat
		{
			public class Adopt
			{
				public Database.Models.Cat Cat { get; set; }
				public string OwnerName { get; set; }
			}

			public class Rename
			{
				public string Id { get; set; }
				public string Name { get; set; }
			}

			public class Message
			{
				public string Username { get; set; }
				public string Content { get; set; }
			}
		}
		
	}
}
