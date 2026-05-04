using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Specification
{
    internal class ConversationsWithMessagesSpec : BaseSpecification<Conversation>
    {
        public ConversationsWithMessagesSpec(Expression<Func<Conversation, bool>> criteria)
            : base(criteria)
        {
            AddInclude(c => c.Messages);
            AddOrderByDescending(c => c.UpdatedAt);
        }
    }
}
