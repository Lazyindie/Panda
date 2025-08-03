using Microsoft.EntityFrameworkCore;
using Panda.EntityFramework;
using Panda.Library.Class.Appointment;

namespace Panda.Services.Appointments.GetAppointment;

public class FindAppointmentService(IDatabaseContext databaseContext) : IFindAppointmentService
{
    public async Task<AppointmentDto> GetAppointmentAsync(Guid appointmentId, CancellationToken cancellationToken = default)
    {
        // Attempt to find the appointment by Id, if it does not exist then throw and return a 404 error.
        var appointment = await databaseContext.Appointments
            .Include((appointment) => appointment.Clinician)
            .Include((appointment) => appointment.Patient)
            .Include((appointment) => appointment.Department)
            .FirstOrDefaultAsync((appointment) => appointment.Id == appointmentId && appointment.DeletedAt == null, cancellationToken);

        if (appointment is null)
        {
            throw new KeyNotFoundException($"Appointment with ID {appointmentId} not found.");
        }

        return new AppointmentDto(
            appointment.Id,
            appointment.Clinician.Name,
            appointment.Patient.Name,
            appointment.Department.Name,
            appointment.Status,
            appointment.Time,
            appointment.Duration);
    }
}
