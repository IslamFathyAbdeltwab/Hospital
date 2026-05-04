using Hosptial.BLL.ViewModels;
using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    public interface IChatService
    {
        Task<ConversationDto> CreateConversation(CreateConversationDto dto);
        Task<List<ConversationDto>> GetPatientConversations(int patientId);
        Task<List<MessageDto>> GetMessages(int conversationId);
        Task<MessageDto> SaveMessage(SendMessageDto dto);
        Task MarkAsRead(int conversationId, int userId);
    }

  
}
