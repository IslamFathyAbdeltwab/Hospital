using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels
{
    // MessageDto.cs
    public class MessageDto
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public int SenderId { get; set; }
        public string SenderRole { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
    }

    // ConversationDto.cs
    public class ConversationDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int AppointmentId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public MessageDto LastMessage { get; set; }
        public int UnreadCount { get; set; }
    }

    // SendMessageDto.cs
    public class SendMessageDto
    {
        public int ConversationId { get; set; }
        public int SenderId { get; set; }
        public string SenderRole { get; set; }
        public string Content { get; set; }
    }

    // CreateConversationDto.cs
    public class CreateConversationDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int AppointmentId { get; set; }
    }
}
