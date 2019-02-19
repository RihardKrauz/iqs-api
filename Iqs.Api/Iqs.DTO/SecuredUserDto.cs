
using System.ComponentModel.DataAnnotations;

namespace Iqs.DTO
{
    public class SecuredUserDto
    {
        [Required]
        [StringLength(50)]
        public string Login { get; set; }
        [Required]
        [StringLength(150, MinimumLength=5)]
        public string Name { get; set; }
        public string Role { get; set; }
        [Required]
        [Range(0, 99)]
        public int Age { get; set; }
    }
}
