namespace Panda.Library.Class.Patient;

public record EditPatientDto(Guid id, string? name, DateOnly? dateOfBirth, string? postcode);