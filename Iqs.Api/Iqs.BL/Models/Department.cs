using System;
using System.Collections.Generic;
using System.Text;

namespace Iqs.BL.Models
{
    public class Department : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
