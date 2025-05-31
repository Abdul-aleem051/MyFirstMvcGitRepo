using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdoDotNetMvc.ViewModel
{
    public class UpdateDepartmentViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description{ get; set; } 
    }
}