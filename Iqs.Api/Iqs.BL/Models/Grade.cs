using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Iqs.DAL.Models
{
    public class Grade : IEntity
    {
        [Key]
        public long Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
    }
}
