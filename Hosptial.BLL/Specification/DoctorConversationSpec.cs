using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Specification
{
    public class DoctorConversationSpec : BaseSpecification<Conversation>
    {
        public DoctorConversationSpec(int doctorId)
            : base(c => c.DoctorId == doctorId)
        {
            AddInclude(c => c.Messages);
            AddOrderByDescending(c => c.UpdatedAt);
        }
    }
}
