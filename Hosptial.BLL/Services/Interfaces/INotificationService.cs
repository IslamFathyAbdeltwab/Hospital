using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    public interface INotificationService
    {
        public  Task SendToDoctor(int doctorId, string message);
    }
}
