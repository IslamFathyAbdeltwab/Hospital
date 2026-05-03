using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.Specification;
using Hosptial.BLL.ViewModels;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Interfaces;

namespace Hosptial.BLL.Services.Classes
{
    public class ChatService : IChatService
    {
        private readonly IUniteOfWork _uniteOfWork;

        public ChatService(IUniteOfWork uniteOfWork)
        {
            _uniteOfWork = uniteOfWork;
        }

        // ── Create Conversation ───────────────────────────────────
        public async Task<ConversationDto> CreateConversation(CreateConversationDto dto)
        {
            // Validate confirmed appointment using spec
            var bookingSpec = new BookingConfirmedSpec(
                dto.AppointmentId, dto.PatientId, dto.DoctorId);

            var appointment = await _uniteOfWork
                .GetGenaricRepo<Booking>()
                .Get(bookingSpec);

            if (appointment == null)
                throw new Exception("No confirmed appointment found.");

            var conversation = new Conversation
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentId = dto.AppointmentId,
                Title = $"Appointment #{dto.AppointmentId}",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _uniteOfWork.GetGenaricRepo<Conversation>().Add(conversation);
            await _uniteOfWork.SaveChangesAsync();

            return MapConversation(conversation, null, 0);
        }

        // ── Get Patient Conversations ─────────────────────────────
        public async Task<List<ConversationDto>> GetPatientConversations(int patientId)
        {
            var spec = new ConversationWithMessagesSpec(patientId);

            var conversations = await _uniteOfWork
                .GetGenaricRepo<Conversation>()
                .GetAll(spec);

            return conversations.Select(c =>
            {
                var last = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault();
                var unread = c.Messages.Count(m => !m.IsRead && m.SenderRole == "Doctor");
                return MapConversation(c, last, unread);
            }).ToList();
        }

        // ── Get Messages ──────────────────────────────────────────
        public async Task<List<MessageDto>> GetMessages(int conversationId)
        {
            var spec = new MessagesByConversationSpec(conversationId);

            var messages = await _uniteOfWork
                .GetGenaricRepo<Message>()
                .GetAll(spec);

            return messages.Select(m => MapMessage(m)).ToList();
        }

        // ── Save Message ──────────────────────────────────────────
        public async Task<MessageDto> SaveMessage(SendMessageDto dto)
        {
            var msg = new Message
            {
                ConversationId = dto.ConversationId,
                SenderId = dto.SenderId,
                SenderRole = dto.SenderRole,
                Content = dto.Content,
                IsRead = false,
                SentAt = DateTime.UtcNow,
            };

            _uniteOfWork.GetGenaricRepo<Message>().Add(msg);

            // Update conversation timestamp
            var convSpec = new ConversationWithMessagesSpec(
                c => c.Id == dto.ConversationId);

            var conv = await _uniteOfWork
                .GetGenaricRepo<Conversation>()
                .Get(convSpec);

            if (conv != null)
                conv.UpdatedAt = DateTime.UtcNow;

            await _uniteOfWork.SaveChangesAsync();
            return MapMessage(msg);
        }

        // ── Mark As Read ──────────────────────────────────────────
        public async Task MarkAsRead(int conversationId, int userId)
        {
            var spec = new UnreadMessagesSpec(conversationId, userId);

            var unreadMessages = await _uniteOfWork
                .GetGenaricRepo<Message>()
                .GetAll(spec);

            foreach (var msg in unreadMessages)
                msg.IsRead = true;

            await _uniteOfWork.SaveChangesAsync();
        }

        // ── Mappers ───────────────────────────────────────────────
        private static ConversationDto MapConversation(
            Conversation c, Message? last, int unread) => new()
            {
                Id = c.Id,
                PatientId = c.PatientId,
                DoctorId = c.DoctorId,
                AppointmentId = c.AppointmentId,
                Title = c.Title,
                CreatedAt = c.CreatedAt,
                LastMessage = last is null ? null : MapMessage(last),
                UnreadCount = unread,
            };

        private static MessageDto MapMessage(Message m) => new()
        {
            Id = m.Id,
            ConversationId = m.ConversationId,
            SenderId = m.SenderId,
            SenderRole = m.SenderRole,
            Content = m.Content,
            IsRead = m.IsRead,
            SentAt = m.SentAt,
        };
    }
}