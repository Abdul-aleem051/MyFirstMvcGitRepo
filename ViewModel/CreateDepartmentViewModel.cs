using System.ComponentModel.DataAnnotations;

namespace AdoDotNetMvc.ViewModel
{
    public class CreateDepartmentViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }
    }
}