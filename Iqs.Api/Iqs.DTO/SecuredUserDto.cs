
using System.ComponentModel.DataAnnotations;

namespace Iqs.DTO
{
    public class SecuredUserDto
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"\S+")]
        public string Login { get; set; }
        [Required]
        [StringLength(150, MinimumLength=5)]
        public string Name { get; set; }
        public string Role { get; set; }
        public long SpecializationId { get; set; }
        public long DepartmentId { get; set; }
        [Required]
        [Range(0, 99)]
        public int Age { get; set; }
    }
}
