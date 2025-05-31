namespace AdoDotNetMvc.ViewModel
{
    public class DepartmentViewModel
    {   
        public Guid Id  { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;
    }
}