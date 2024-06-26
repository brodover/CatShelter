using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using WithAngularApp.Server.Data.Models;

namespace WithAngularApp.Server.Hubs
{
	public class ChatHub : Hub
	{
		public static ConcurrentDictionary<string, string> myUsers = new ConcurrentDictionary<string, string>();

		private readonly ILogger<ChatHub> _logger;

		public ChatHub(ILogger<ChatHub> logger)
		{
			_logger = logger;
		}


		public override Task OnConnectedAsync()
		{
			_logger.LogDebug("Client connected: {ConnectionId}", Context.ConnectionId);
			myUsers.TryAdd(Context.ConnectionId, Context.ConnectionId);
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception? exception)
		{
			_logger.LogDebug("Client disconnected: {ConnectionId}", Context.ConnectionId);
			if (exception != null)
			{
				_logger.LogError(exception, "Client disconnected with error");
			}
			return base.OnDisconnectedAsync(exception);
		}

		public async Task SendMessage(string username, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", username, message);
		}


	}
}
