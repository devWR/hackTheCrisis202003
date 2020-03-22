using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Hubs
{
    public class ChatHub : Hub
    {
        static HashSet<string> ConnectionsAwaiting = new HashSet<string>();

        //The key is person who looks for help and value consultant.
        static Dictionary<string, string> OcupatedConnections = new Dictionary<string, string>();

        public async Task SendToAll(string name, string message)
        {
            await Clients.All.SendAsync("send", name, message);
        }

        public async Task SendTo(string connId, string name, string message)
        {
            await Clients.Client(connId).SendAsync("send", name, message);
        }

        public async Task ReceiveConnId()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("receiveConnId", Context.ConnectionId);
        }

        public async Task SendConsultantId(string toWhom, string consultantId)
        {
            await Clients.Client(toWhom).SendAsync("receiveConsultantConnId", consultantId);
        }

        public async Task FinishConversation(string toWhom)
        {
            await Clients.Client(toWhom).SendAsync("conversationFinished");
        }

        public async Task EnableDetailedFormFill(string toWhom, bool ifEnable)
        {
            await Clients.Client(toWhom).SendAsync("enableDetailedFormFill", ifEnable);
        }

        public static List<string> GetNotOcupatedConnections()
        {
            return ConnectionsAwaiting.Where(e => !OcupatedConnections.Keys.Contains(e)).ToList();
        }

        public static void AddToOcupated(string personId, string consultantId)
        {
            if (!ConnectionsAwaiting.Contains(personId))
                throw new Exception($"Such id: {personId} does not exist!");

            OcupatedConnections.Add(personId, consultantId);
        }

        public override Task OnConnectedAsync()
        {
            var id = Context.ConnectionId;

            if (!ConnectionsAwaiting.Contains(id))
                ConnectionsAwaiting.Add(id);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (Global.UserInitialForms.ContainsKey(Context.ConnectionId))
                Global.UserInitialForms.Remove(Context.ConnectionId);

            if (ConnectionsAwaiting.Contains(Context.ConnectionId))
                ConnectionsAwaiting.Remove(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
