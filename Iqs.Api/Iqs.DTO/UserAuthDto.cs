
using System.ComponentModel.DataAnnotations;

namespace Iqs.DTO
{
    public class UserAuthDto
    {
        public SecuredUserDto User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
