using Panda.Library.Class.Appointment;

namespace Panda.Services.Appointments.GetAppointment;

public interface IFindAppointmentService
{
    public Task<AppointmentDto> GetAppointmentAsync(Guid appointmentId, CancellationToken cancellationToken = default);
}
