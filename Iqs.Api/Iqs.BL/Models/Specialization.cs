using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Iqs.DAL.Models
{
    public class Specialization : IEntity
    {
        [Key]
        public long Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}
