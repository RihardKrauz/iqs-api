using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Iqs.DAL.Models
{
    public interface IEntity
    {
        [Key]
        long Id { get; set; }
    }
}
