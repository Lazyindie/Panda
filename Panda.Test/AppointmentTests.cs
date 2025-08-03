using Panda.Domain;
using Panda.EntityFramework;
using Panda.Library.Class.Appointment;
using Panda.Library.Class.Clinician;
using Panda.Library.Class.Common;
using Panda.Services.Appointments.AddAppointment;
using Panda.Services.Appointments.GetAppointment;
using Panda.Services.Appointments.UpdateAppointmentStatus;
using Panda.Test.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Panda.Test;

public class AppointmentTests
{
    private readonly DatabaseContext _databaseContext;
    private readonly AddAppointmentService _addAppointmentService;
    private readonly EditAppointmentService _editAppointmentService;
    private readonly FindAppointmentService _getAppointmentService;

    public AppointmentTests()
    {
        _databaseContext = DatabaseContextFactory.CreateDatabase();
        _addAppointmentService = new AddAppointmentService(_databaseContext);
        _editAppointmentService = new EditAppointmentService(_databaseContext);
        _getAppointmentService = new FindAppointmentService(_databaseContext);
    }

    [Fact]
    public async Task Can_Create_Appointment()
    {
        // Arrange
        (Guid patientId, Guid clinicianId, Guid departmentId) = PrepareAppointment();
        var addAppointmentDto = new AddAppointmentDto(clinicianId, patientId, departmentId, DateTimeOffset.UtcNow, TimeOnly.MinValue);

        // Act
        var appointmentId = await _addAppointmentService.AddAppointmentAsync(addAppointmentDto, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, appointmentId);
    }

    [Fact]
    public async Task Appointment_Defaults_To_Active()
    {
        // Arrange
        (Guid patientId, Guid clinicianId, Guid departmentId) = PrepareAppointment();
        var addAppointmentDto = new AddAppointmentDto(clinicianId, patientId, departmentId, DateTimeOffset.UtcNow, TimeOnly.MinValue);

        // Act
        var appointmentId = await _addAppointmentService.AddAppointmentAsync(addAppointmentDto, CancellationToken.None);
        var appointment = await _databaseContext.Appointments.FindAsync(appointmentId, CancellationToken.None);

        // Assert
        Assert.NotNull(appointment);
        Assert.Equal(AppointmentStatus.Active, appointment.Status);
    }

    [Fact]
    public async Task Can_Get_Appointment()
    {
        // Arrange
        (Guid patientId, Guid clinicianId, Guid departmentId) = PrepareAppointment();
        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            PatientId = patientId,
            ClinicianId = clinicianId,
            DepartmentId = departmentId,
            Time = DateTimeOffset.UtcNow,
            Duration = TimeOnly.MinValue,
        };
        await _databaseContext.Appointments.AddAsync(appointment);
        await _databaseContext.SaveChangesAsync(CancellationToken.None);

        // Act
        var found = await _getAppointmentService.GetAppointmentAsync(appointment.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(found);
    }

    [Fact]
    public async Task Cannot_Get_Deleted_Appointment()
    {
        // Arrange
        (Guid patientId, Guid clinicianId, Guid departmentId) = PrepareAppointment();
        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            PatientId = patientId,
            ClinicianId = clinicianId,
            DepartmentId = departmentId,
            Time = DateTimeOffset.UtcNow,
            Duration = TimeOnly.MinValue,
            DeletedAt = DateTimeOffset.UtcNow
        };

        await _databaseContext.Appointments.AddAsync(appointment);
        await _databaseContext.SaveChangesAsync(CancellationToken.None);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _getAppointmentService.GetAppointmentAsync(appointment.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Can_Edit_Appointment()
    {
        // Arrange
        (Guid patientId, Guid clinicianId, Guid departmentId) = PrepareAppointment();
        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            PatientId = patientId,
            ClinicianId = clinicianId,
            DepartmentId = departmentId,
            Time = DateTimeOffset.UtcNow,
            Duration = TimeOnly.MinValue,
        };
        await _databaseContext.Appointments.AddAsync(appointment);
        await _databaseContext.SaveChangesAsync(CancellationToken.None);

        // Act
        (Guid editPatientId, Guid editClinicianId, Guid editDepartmentId) = PrepareAppointment();
        var editAppointmentDto = new EditAppointmentDto(
            appointment.Id,
            editClinicianId,
            editDepartmentId,
            AppointmentStatus.Active,
            DateTimeOffset.UtcNow.AddHours(1),
            TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(30))
        );
        await _editAppointmentService.EditAppointmentAsync(editAppointmentDto, CancellationToken.None);

        // Assert
        Assert.Equal(appointment.ClinicianId, editClinicianId);
        Assert.Equal(appointment.DepartmentId, editDepartmentId);
    }

    [Fact]
    public async Task Cannot_Edit_Deleted_Appointment()
    {
        // Arrange
        (Guid patientId, Guid clinicianId, Guid departmentId) = PrepareAppointment();
        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            PatientId = patientId,
            ClinicianId = clinicianId,
            DepartmentId = departmentId,
            Time = DateTimeOffset.UtcNow,
            Duration = TimeOnly.MinValue,
            DeletedAt = DateTimeOffset.UtcNow
        };
        await _databaseContext.Appointments.AddAsync(appointment);
        await _databaseContext.SaveChangesAsync(CancellationToken.None);

        // Act & Assert
        (Guid editPatientId, Guid editClinicianId, Guid editDepartmentId) = PrepareAppointment();
        var editAppointmentDto = new EditAppointmentDto(
            appointment.Id,
            editClinicianId,
            editDepartmentId,
            AppointmentStatus.Active,
            DateTimeOffset.UtcNow.AddHours(1),
            TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(30))
        );

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _editAppointmentService.EditAppointmentAsync(editAppointmentDto, CancellationToken.None));
    }

    private (Guid patientId, Guid clinicianId, Guid departmentId) PrepareAppointment()
    {
        var patientId = Guid.NewGuid();
        var clinicianId = Guid.NewGuid();
        var departmentId = Guid.NewGuid();

        // Add a patient
        _databaseContext.AddAsync(new Patient
        {
            Id = patientId,
            Name = "John Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Postcode = "12345"
        });

        // Add a clinician
        _databaseContext.AddAsync(new Clinician
        {
            Id = clinicianId,
            Name = "Dr. Smith",
            DateOfBirth = new DateOnly(1980, 1, 1),
            DepartmentId = departmentId
        });

        // Add a department
        _databaseContext.AddAsync(new Department
        {
            Id = departmentId,
            Name = Guid.NewGuid().ToString()
        });

        return (patientId, clinicianId, departmentId);

    }
}
