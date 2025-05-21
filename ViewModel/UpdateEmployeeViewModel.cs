using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoDotNetMvc.ViewModel
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Department { get; set; } = default!;
        public string Salary { get; set; } = default!;
        public DateTime HireDate { get; set; }


    }
}