using Hosptial.BLL.Specification;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Classes;
using System.Linq.Expressions;

public class ConversationWithMessagesSpec : BaseSpecification<Conversation>
{
    // Get all conversations for a patient with messages included
    public ConversationWithMessagesSpec(int patientId)
        : base(c => c.PatientId == patientId)
    {
        AddInclude(c => c.Messages);
        AddOrderByDescending(c => c.UpdatedAt);
    }

    // Get single conversation by id with messages
    public ConversationWithMessagesSpec(Expression<Func<Conversation, bool>> criteria)
        : base(criteria)
    {
        AddInclude(c => c.Messages);
    }
}