using System.ComponentModel.DataAnnotations;

namespace AdoDotNetMvc.ViewModel
{
    public class CreateEmployeeViewModel
    {
        [Required(ErrorMessage = "Employee's firstname is required!")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "Employee's lastname is required!")]
        public string LastName { get; set; } = default!;

        [Required(ErrorMessage = "Select a department")]
        public string Department { get; set; } = default!;

        [Required(ErrorMessage = "Invalid Salary value!")]
        public decimal Salary { get; set; } = default!;

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "HireDate is required!")]

        public DateTime HireDate { get; set; }
    }
}