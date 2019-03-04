
namespace Iqs.DTO
{
    public class EmployeeDto : SecuredUserDto
    {
        public SpecializationDto Specialization { get; set; }
        public DepartmentDto Department { get; set; }
        public GradeDto Grade { get; set; }
    }
}
