using Panda.Library.Class.Appointment;

namespace Panda.Services.Appointments.AddAppointment;

public interface IAddAppointmentService
{
    public Task<Guid> AddAppointmentAsync(AddAppointmentDto request, CancellationToken cancellationToken);
}
