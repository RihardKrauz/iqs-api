using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Iqs.DAL.Models
{
    public class Department : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
