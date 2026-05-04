using Microsoft.EntityFrameworkCore;

[Keyless]
public class DoctorDashboardDto
{
    public int TotalPatients { get; set; }
    public int TotalPrescriptions { get; set; }
    public int TotalBookings { get; set; }
    public int ConfirmedBookings { get; set; }
    public int PendingBookings { get; set; }
    public int CancelledBookings { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal PendingRevenue { get; set; }
    public decimal ThisMonthRevenue { get; set; }
    public decimal AvgBookingPrice { get; set; }
}