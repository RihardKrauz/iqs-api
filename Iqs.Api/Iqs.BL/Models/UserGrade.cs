using System;
using System.Collections.Generic;
using System.Text;

namespace Iqs.BL.Models
{
    public class UserGrade : IEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long GradeId { get; set; }
        public DateTime QualifiedDate { get; set; }
    }
}
