using Panda.Library.Class.Department;

namespace Panda.Services.Department.AddDepartment;

public interface IAddDepartmentService
{
    public Task<Guid> AddDepartmentAsync(AddDepartmentDto request, CancellationToken cancellationToken);
}
