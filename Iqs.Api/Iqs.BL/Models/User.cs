using System;
using System.Collections.Generic;
using System.Text;

namespace Iqs.DAL.Models
{
    public class User : IEntity
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public string PassHash { get; set; }
        public string Salt { get; set; }
        public string RefreshToken { get; set; }
        public long DepartmentId { get; set; }
    }
}
