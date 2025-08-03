using Panda.Library.Class.Common;

namespace Panda.Library.Class.Appointment;

/// <summary>
/// Update appointment details.
/// </summary>
/// <param name="appointmentId">The Id of the appointment to update.</param>
/// <param name="clinicianId">The Id of the new clinician attending the appointment.</param>
/// <param name="departmentId">The id of the new department the appointment is registered.</param>
/// <param name="status">The status of the appointment.</param>
/// <param name="time">The time of the appointment.</param>
/// <param name="duration">The duratuib of the appointment.</param>
public record EditAppointmentDto(Guid appointmentId, Guid? clinicianId, Guid? departmentId, AppointmentStatus? status, DateTimeOffset? time, TimeOnly? duration);