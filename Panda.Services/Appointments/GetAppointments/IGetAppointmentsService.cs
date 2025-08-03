using Panda.Library.Class.Appointment;

namespace Panda.Services.Appointments.GetAppointments;
public interface IGetAppointmentsService
{
    public Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync(CancellationToken cancellationToken = default);
}
