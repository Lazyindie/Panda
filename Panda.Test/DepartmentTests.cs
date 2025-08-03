using Panda.EntityFramework;
using Panda.Library.Class.Clinician;
using Panda.Library.Class.Department;
using Panda.Services.Department.AddDepartment;
using Panda.Services.Members.Clinicians.AddClinician;
using Panda.Services.Members.Clinicians.GetClinicians;
using Panda.Test.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Panda.Test;
public class DepartmentTests
{
    private readonly DatabaseContext _databaseContext;
    private readonly AddDepartmentService _addDepartmentService;

    public DepartmentTests()
    {
        _databaseContext = DatabaseContextFactory.CreateDatabase();
        _addDepartmentService = new AddDepartmentService(_databaseContext);
    }

    [Fact]
    public async Task Can_Create_Department()
    {
        // Arrange
        var addDepartmentDto = new AddDepartmentDto("Cardiology", "Heart-related treatments and surgeries");

        // Act
        var clinicianId = await _addDepartmentService.AddDepartmentAsync(addDepartmentDto, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, clinicianId);
    }
}
