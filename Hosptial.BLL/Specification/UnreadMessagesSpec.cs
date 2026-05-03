using Hosptial.BLL.Specification;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Classes;

public class UnreadMessagesSpec : BaseSpecification<Message>
{
    public UnreadMessagesSpec(int conversationId, int userId)
        : base(m =>
            m.ConversationId == conversationId &&
            !m.IsRead &&
            m.SenderId != userId)
    {
    }
}