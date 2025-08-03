using Microsoft.EntityFrameworkCore;
using Panda.EntityFramework;
using Panda.Library.Class.Appointment;

namespace Panda.Services.Appointments.GetAppointments;

public class GetAppointmentsService(IDatabaseContext databaseContext) : IGetAppointmentsService
{
    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.Appointments
            .Include((appointment) => appointment.Clinician)
            .Include((appointment) => appointment.Patient)
            .Include((appointment) => appointment.Department)
            .AsNoTracking()
            .Where((appointment) => appointment.DeletedAt == null)
            .Select(appointment => new AppointmentDto(appointment.Id, appointment.Clinician.Name, appointment.Patient.Name, appointment.Department.Name, appointment.Status, appointment.Time, appointment.Duration))
            .ToListAsync(cancellationToken);
    }
}
