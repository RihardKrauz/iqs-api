using Iqs.BL.Infrastructure;
using Iqs.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iqs.BL.Interfaces
{
    public interface IDictionariesEngine
    {
        MethodResult<List<SpecializationDto>> GetSpecializations();
        MethodResult<List<DepartmentDto>> GetDepartments();
    }
}
