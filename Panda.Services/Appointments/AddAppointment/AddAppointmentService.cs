using Panda.Domain;
using Panda.EntityFramework;
using Panda.Library.Class.Appointment;

namespace Panda.Services.Appointments.AddAppointment;

public class AddAppointmentService(IDatabaseContext databaseContext) : IAddAppointmentService
{
    public async Task<Guid> AddAppointmentAsync(AddAppointmentDto request, CancellationToken cancellationToken)
    {
        var appointment = new Appointment
        {
            PatientId = request.patientId,
            ClinicianId = request.clinicianId,
            DepartmentId = request.departmentId,
            Time = request.time,
            Duration = request.duration
        };

        await databaseContext.Appointments.AddAsync(appointment);
        await databaseContext.SaveChangesAsync(cancellationToken);

        return appointment.Id;
    }
}
