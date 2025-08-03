namespace Panda.Library.Class.Appointment;

public record AddAppointmentDto(Guid clinicianId, Guid patientId, Guid departmentId, DateTimeOffset time, TimeOnly duration);