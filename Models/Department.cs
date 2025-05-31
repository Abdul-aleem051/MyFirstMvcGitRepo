using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdoDotNetMvc.Models
{
    public class Department
    {
        public Guid Id { get; set; } 

        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }
    }
}