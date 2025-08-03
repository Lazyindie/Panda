namespace Panda.Library.Class.Clinician;

public record AddClinicianDto(string name, DateOnly dateOfBirth, Guid departmentId);