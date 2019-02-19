
using System.ComponentModel.DataAnnotations;

namespace Iqs.DTO
{
    public class AuthDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
