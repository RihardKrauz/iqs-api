using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Iqs.DAL.Models
{
    public class UserGrade : IEntity
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public long GradeId { get; set; }
        public DateTime QualifiedDate { get; set; }
    }
}
