using Microsoft.EntityFrameworkCore;
using Panda.EntityFramework;
using Panda.Library.Class.Appointment;
using Panda.Library.Class.Common;

namespace Panda.Services.Appointments.UpdateAppointmentStatus;

public class EditAppointmentService(IDatabaseContext databaseContext) : IEditAppointmentService
{
    public async Task EditAppointmentAsync(EditAppointmentDto request, CancellationToken cancellationToken = default)
    {
        // Attempt to find the appointment by Id, if it does not exist then throw and return a 404 error.
        var appointment = await databaseContext.Appointments
            .FirstOrDefaultAsync((appointment) => appointment.Id == request.appointmentId && appointment.DeletedAt == null, cancellationToken);

        if (appointment is null)
        {
            throw new KeyNotFoundException($"Appointment with ID {request.appointmentId} not found.");
        }

        // If the appointment is cancelled, we cannot update its status and return a 400 error.
        if (appointment.Status == AppointmentStatus.Cancelled)
        {
            throw new ArgumentException("Cannot update the status of a cancelled appointment.", nameof(request.appointmentId));
        }

        // Update the appointment's status and other detail.
        // If the changes value is not provided, it will retain the current value.
        appointment.Status = request.status ?? appointment.Status;
        appointment.DepartmentId = request.departmentId ?? appointment.DepartmentId;
        appointment.ClinicianId = request.clinicianId ?? appointment.ClinicianId;
        appointment.Time = request.time ?? appointment.Time;
        appointment.Duration = request.duration ?? appointment.Duration;

        databaseContext.Appointments.Update(appointment);

        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
