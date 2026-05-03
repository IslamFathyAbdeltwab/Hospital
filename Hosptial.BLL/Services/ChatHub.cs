using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services
{
    // ChatHub.cs
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        // Patient/Doctor joins their conversation room
        public async Task JoinConversation(int conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"conv_{conversationId}");
        }

        public async Task LeaveConversation(int conversationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"conv_{conversationId}");
        }

        // Send a message — saves to DB and broadcasts to the group
        public async Task SendMessage(SendMessageDto dto)
        {
            var saved = await _chatService.SaveMessage(dto);
            await Clients.Group($"conv_{dto.ConversationId}")
                         .SendAsync("ReceiveMessage", saved);
        }

        // Mark messages as read
        public async Task MarkRead(int conversationId, int userId)
        {
            await _chatService.MarkAsRead(conversationId, userId);
            await Clients.Group($"conv_{conversationId}")
                         .SendAsync("MessagesRead", conversationId, userId);
        }
    }

}
