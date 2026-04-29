using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels
{
    public class PaymentDto
    {
        public decimal Amount { get; set; }
        public int BookingId { get; set; }
    }
}
