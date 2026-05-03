using Hosptial.BLL.Specification;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Classes;

public class BookingConfirmedSpec : BaseSpecification<Booking>
{
    public BookingConfirmedSpec(int appointmentId, int patientId, int doctorId)
        : base(a =>
            a.Id == appointmentId &&
            a.PatientId == patientId &&
            a.DoctorAvailability.DoctorId == doctorId &&
            a.Status == AppointmentStatus.Confirmed)
    {
        AddInclude(a => a.DoctorAvailability);
    }
}