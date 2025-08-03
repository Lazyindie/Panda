namespace Panda.Library.Class.Common;

/// <summary>
/// An enum representing the status of a scheduled appointment.
/// </summary>
public enum AppointmentStatus {
    // The appointment is scheduled and active.
    Active = 0,

    // The appointment has been completed successfully.
    Attended = 100,

    // The appointment was missed by the patient.
    Missed = 200,

    // The appointment was cancelled. 
    Cancelled = 300,
}