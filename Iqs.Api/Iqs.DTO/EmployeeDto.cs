
using System.Collections.Generic;

namespace Iqs.DTO
{
    public class EmployeeDto : SecuredUserDto
    {
        public SpecializationDto Specialization { get; set; }
        public DepartmentDto Department { get; set; }
        public List<UserGradeDto> Qualification { get; set; }
    }
}
