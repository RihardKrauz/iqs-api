
namespace Iqs.DTO
{
    public class EmployeeDto : SecuredUserDto
    {
        public DepartmentDto Department { get; set; }
        public GradeDto Grade { get; set; }
    }
}
