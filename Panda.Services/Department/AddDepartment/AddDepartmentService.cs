using Panda.EntityFramework;
using Panda.Library.Class.Department;

namespace Panda.Services.Department.AddDepartment;

public class AddDepartmentService(IDatabaseContext databaseContext) : IAddDepartmentService
{
    public async Task<Guid> AddDepartmentAsync(AddDepartmentDto request, CancellationToken cancellationToken)
    {
        var department = new Domain.Department
        {
            Name = request.name,
            Description = request.description
        };

        await databaseContext.Departments.AddAsync(department, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);

        return department.Id;
    }
}
