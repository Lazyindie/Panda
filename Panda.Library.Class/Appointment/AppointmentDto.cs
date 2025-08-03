using Panda.Library.Class.Common;

namespace Panda.Library.Class.Appointment;

/// <summary>
/// A record representing an appointment.
/// </summary>
/// <param name="id">The id of the appointment</param>
/// <param name="clinician">The name of the registered clinician.</param>
/// <param name="patient">The name of the requested patient.</param>
/// <param name="department">The name of the corrisponding department.</param>
/// <param name="status">The status of the appointment.</param>
/// <param name="time">The date and time of the appointment.</param>
/// <param name="duration">The length of the appointment.</param>
public record AppointmentDto(Guid id, string clinician, string patient, string department, AppointmentStatus? status, DateTimeOffset? time, TimeOnly? duration);