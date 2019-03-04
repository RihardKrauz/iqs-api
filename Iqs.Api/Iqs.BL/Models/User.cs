using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Iqs.DAL.Models
{
    public class User : IEntity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Login { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        public int Age { get; set; }
        [StringLength(50)]
        public string Role { get; set; }
        public string PassHash { get; set; }
        public string Salt { get; set; }
        public string RefreshToken { get; set; }
        [ForeignKey("Department")]
        public long DepartmentId { get; set; }
        public Department Department { get; set; }
        [ForeignKey("Specialization")]
        public long SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
