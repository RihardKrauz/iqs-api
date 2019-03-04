using Iqs.BL.Infrastructure;
using Iqs.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iqs.BL.Interfaces
{
    public interface IGradesEngine
    {
        MethodResult<List<GradeDto>> GetGradesBySpecialization(long specId);
    }
}
