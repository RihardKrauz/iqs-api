using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Iqs.DAL.Models
{
    public class UserGrade : IEntity
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Grade")]
        public long GradeId { get; set; }
        public Grade Grade { get; set; }
        public DateTime QualifiedDate { get; set; }
    }
}
