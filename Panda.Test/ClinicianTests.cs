using Panda.Domain;
using Panda.EntityFramework;
using Panda.Library.Class.Clinician;
using Panda.Services.Members.Clinicians.AddClinician;
using Panda.Services.Members.Clinicians.GetClinicians;
using Panda.Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Panda.Test;

public sealed class ClinicianTests
{
    private readonly DatabaseContext _databaseContext;
    private readonly AddClinicianService _addClinicianService;
    private readonly GetCliniciansService _getClinicianService;

    public ClinicianTests()
    {
        _databaseContext = DatabaseContextFactory.CreateDatabase();
        _addClinicianService = new AddClinicianService(_databaseContext);
        _getClinicianService = new GetCliniciansService(_databaseContext);
    }

    [Fact]
    public async Task Can_Create_Clinician()
    {
        // Arrange
        var departmentId = await AddDepartment();
        var addClinicianDto = new AddClinicianDto("Dr. Smith", new DateOnly(1980, 1, 1), departmentId);

        // Act
        var clinicianId = await _addClinicianService.AddClinicianAsync(addClinicianDto, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, clinicianId);
    }

    [Fact]
    public async Task Cannot_Create_Clinician_Without_Department()
    {
        // Arrange
        var addClinicianDto = new AddClinicianDto("Dr. Smith", new DateOnly(1980, 1, 1), Guid.Empty);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _addClinicianService.AddClinicianAsync(addClinicianDto, CancellationToken.None));
    }

    [Fact]
    public async Task Can_Get_Clinician()
    {
        // Arrange
        var departmentId = await AddDepartment();
        var clinician = new Clinician
        {
            Id = Guid.NewGuid(),
            Name = "Dr. Smith",
            DateOfBirth = new DateOnly(1980, 1, 1),
            DepartmentId = departmentId
        };

        await _databaseContext.Clinicians.AddAsync(clinician);
        await _databaseContext.SaveChangesAsync();

        // Act
        var found = await _getClinicianService.GetCliniciansAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(found);
    }

    [Fact]
    public async Task Cannot_Get_Deleted_Clinicians()
    {
        // Arrange
        var departmentId = await AddDepartment();
        var clinician = new Clinician
        {
            Id = Guid.NewGuid(),
            Name = "Dr. Smith",
            DateOfBirth = new DateOnly(1980, 1, 1),
            DepartmentId = departmentId,
            DeletedAt = DateTime.UtcNow
        };

        await _databaseContext.Clinicians.AddAsync(clinician);
        await _databaseContext.SaveChangesAsync();

        // Act
        var result = await _getClinicianService.GetCliniciansAsync(CancellationToken.None);

        // Assert
        Assert.False(result.Any());
    }

    [Fact]
    public async Task Get_Deleted_Clinicians_Without_Deleted()
    {
        // Arrange
        var departmentId = await AddDepartment();
        var clinicianA = new Clinician
        {
            Id = Guid.NewGuid(),
            Name = "Dr. Smith",
            DateOfBirth = new DateOnly(1980, 1, 1),
            DepartmentId = departmentId,
            DeletedAt = DateTime.UtcNow
        };
        var clinicianB = new Clinician
        {
            Id = Guid.NewGuid(),
            Name = "Dr. Smith",
            DateOfBirth = new DateOnly(1980, 1, 1),
            DepartmentId = departmentId
        };

        await _databaseContext.Clinicians.AddRangeAsync(clinicianA, clinicianB);
        await _databaseContext.SaveChangesAsync();

        // Act
        var result = await _getClinicianService.GetCliniciansAsync(CancellationToken.None);

        // Assert
        Assert.True(result.Any());
        Assert.Single(result);
    }

    private async Task<Guid> AddDepartment()
    {
        var department = new Department
        {
            Id = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString(), // Use a unique, random name for the department
            Description = "Test Department"
        };

        await _databaseContext.Departments.AddAsync(department);
        await _databaseContext.SaveChangesAsync();

        return department.Id;
    }
}
