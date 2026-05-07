using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services
{
    public class NotificationHub:Hub
    {
        public async Task SendNotificationToDoctor(int doctorId, object notification)
        {
            await Clients.User(doctorId.ToString())
                .SendAsync("ReceiveNotification", notification);
        }
    }
}
