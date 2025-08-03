using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.EntityFrameworkCore;
using Panda.Domain;
using Panda.EntityFramework;
using Panda.Library.Class.Patient;
using Panda.Services.Members.Patients.AddPatient;
using Panda.Services.Members.Patients.EditPatient;
using Panda.Services.Members.Patients.GetPatient;
using Panda.Test.Common;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Panda.Test;
public sealed class PatientTests
{
    private readonly DatabaseContext _databaseContext;
    private readonly GetPatientService _getPatientService;
    private readonly AddPatientService _addPatientService;
    private readonly EditPatientService _editPatientService;

    public PatientTests()
    {
        _databaseContext = DatabaseContextFactory.CreateDatabase();
        _getPatientService = new GetPatientService(_databaseContext);
        _addPatientService = new AddPatientService(_databaseContext);
        _editPatientService = new EditPatientService(_databaseContext);
    }

    [Fact]
    public async Task Can_Create_Patient()
    {
        // Arrange
        var patient = new AddPatientDto("John Doe", new DateOnly(1990, 1, 1), "AB1 2CD");

        // Act
        var patientId = await _addPatientService.AddPatientAsync(patient, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, patientId);
    }

    [Fact]
    public async Task Can_Get_Patient()
    {
        // Arrange
        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Postcode = "AB1 2CD"
        };
        await _databaseContext.Patients.AddAsync(patient);
        await _databaseContext.SaveChangesAsync(CancellationToken.None);

        // Act
        var found = await _getPatientService.GetPatientAsync(patient.Id, CancellationToken.None);

        Assert.NotNull(found);
        Assert.Equal(found.name, patient.Name);
        Assert.Equal(found.dateOfBirth, patient.DateOfBirth);
        Assert.Equal(found.postcode, patient.Postcode);
    }

    [Fact]
    public async Task Cannot_Get_Deleted_Patient()
    {
        // Arrange
        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Postcode = "AB1 2CD",
            DeletedAt = DateTime.UtcNow
        };
        await _databaseContext.Patients.AddAsync(patient);
        await _databaseContext.SaveChangesAsync(CancellationToken.None);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _getPatientService.GetPatientAsync(patient.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Can_Edit_Patient()
    {
        // Arrange
        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Postcode = "AB1 2CD"
        };
        await _databaseContext.Patients.AddAsync(patient);
        await _databaseContext.SaveChangesAsync(CancellationToken.None);

        // Act
        var editPatient = new EditPatientDto(patient.Id, "Jane Doe", new DateOnly(1992, 2, 2), "EF3 4GH");
        await _editPatientService.EditPatient(editPatient, CancellationToken.None);

        // Assert
        Assert.Equal(patient.Name, editPatient.name);
        Assert.Equal(patient.DateOfBirth, editPatient.dateOfBirth);
        Assert.Equal(patient.Postcode, editPatient.postcode);
    }

    [Fact]
    public async Task Can_Edit_Patient_Name_Only()
    {
        // Arrange
        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Postcode = "AB1 2CD"
        };
        await _databaseContext.Patients.AddAsync(patient);
        await _databaseContext.SaveChangesAsync(CancellationToken.None);

        // Act
        var editPatient = new EditPatientDto(patient.Id, "Jane Doe", null, null);
        await _editPatientService.EditPatient(editPatient, CancellationToken.None);

        // Assert
        Assert.NotNull(editPatient);
        Assert.Equal(patient.Name, editPatient.name);
        Assert.NotNull(patient.DateOfBirth);
        Assert.NotNull(patient.Postcode);
    }

    [Fact]
    public async Task Can_Edit_Patient_DOB_Only()
    {
        // Arrange
        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Postcode = "AB1 2CD"
        };
        await _databaseContext.Patients.AddAsync(patient);
        await _databaseContext.SaveChangesAsync(CancellationToken.None);

        // Act
        var editPatient = new EditPatientDto(patient.Id, null, DateOnly.FromDateTime(DateTime.Now), null);
        await _editPatientService.EditPatient(editPatient, CancellationToken.None);

        var changedPatient = await _getPatientService.GetPatientAsync(patient.Id, CancellationToken.None);

        // Assert
        Assert.Equal(patient.DateOfBirth, changedPatient.dateOfBirth);
        Assert.NotNull(patient.Name);
        Assert.NotNull(patient.Postcode);
    }

    [Fact]
    public async Task Can_Edit_Patient_Postcode_Only()
    {
        // Arrange
        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Postcode = "AB1 2CD"
        };
        await _databaseContext.Patients.AddAsync(patient);
        await _databaseContext.SaveChangesAsync(CancellationToken.None);

        // Act
        var changedPatient = new EditPatientDto(patient.Id, null, null, "DC2 1BA");
        await _editPatientService.EditPatient(changedPatient, CancellationToken.None);

        // Assert
        Assert.Equal(patient.Postcode, changedPatient.postcode);
        Assert.NotNull(patient.Name);
        Assert.NotNull(patient.DateOfBirth);
    }

    [Fact]
    public async Task Cannot_Edit_Deleted_Patient()
    {
        // Arrange
        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Postcode = "AB1 2CD",
            DeletedAt = DateTime.UtcNow
        };
        await _databaseContext.Patients.AddAsync(patient);
        await _databaseContext.SaveChangesAsync(CancellationToken.None);

        // Act
        var editPatient = new EditPatientDto(patient.Id, "Jane Doe", new DateOnly(1992, 2, 2), "EF3 4GH");
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _editPatientService.EditPatient(editPatient, CancellationToken.None));
    }
}
