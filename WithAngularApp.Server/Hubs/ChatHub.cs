using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using WithAngularApp.Server.Data.Models;

namespace WithAngularApp.Server.Hubs
{
	public class ChatHub : Hub
	{
		public static ConcurrentDictionary<string, string> myUsers = new ConcurrentDictionary<string, string>();

		public override Task OnConnectedAsync()
		{
			Console.WriteLine($"OnConnectedAsync: {Context.ConnectionId}");
			myUsers.TryAdd(Context.ConnectionId, Context.ConnectionId);
			return base.OnConnectedAsync();
		}

		public async Task SendMessage(string username, string message) =>
			await Clients.All.SendAsync("ReceiveMessage", new Request.Message()
			{
				Username = username,
				Content = message
			});
	}
}
