namespace WithAngularApp.Server.Data.Models
{
	public class Request
	{
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
