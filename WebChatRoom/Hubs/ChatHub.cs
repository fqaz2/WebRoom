using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebChatRoom.Models;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMemoryCache _memoryCache;
        private const string ROOMCACHEKEY = "RoomCacheKey";

        public async Task SendMessage(string roomId, string user, string message)
        {
            await Clients.Group(roomId).SendAsync("ReceiveMessage", user, message);
        }
        public async Task Change(string roomId, string user, string id)
        {
            await Clients.Group(roomId).SendAsync("Clicking", user, id);
        }
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }
        public async Task UpdateRoom(string roomId, string user)
        {
            
            await Clients.Group(roomId).SendAsync("RoomUpdate", user);
        }
        public async Task ParticipantTyping(string roomId, string user)
        {
            await Clients.GroupExcept(roomId, Context.ConnectionId).SendAsync("Typing", user);
        }
        public async Task DisposeRoom(string roomId, string user)
        {
            List<Room> Rooms = _memoryCache.Get<List<Room>>(ROOMCACHEKEY);
            var theRoom = Rooms.Find(r => r.Id.ToString() == roomId);
            Rooms.Remove(theRoom);
        }
    }
}