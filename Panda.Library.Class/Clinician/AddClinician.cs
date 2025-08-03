using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Services.Members.Clinicians.AddClinicians;

public class AddClinician(IDatabaseContext) : IAddClinician
{
    public Task<bool> AddClinicianAsync(AddClinicianDto clinician, CancellationToken cancellationToken)
    {

    }
}
