using Hosptial.BLL.Specification;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Classes;

public class MessagesByConversationSpec : BaseSpecification<Message>
{
    public MessagesByConversationSpec(int conversationId)
        : base(m => m.ConversationId == conversationId)
    {
        AddOrderBy(m => m.SentAt);
    }
}