using Panda.Library.Class.Appointment;

namespace Panda.Services.Appointments.UpdateAppointmentStatus;
public interface IEditAppointmentService
{
    public Task EditAppointmentAsync(EditAppointmentDto request, CancellationToken cancellationToken = default);
}
