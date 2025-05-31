namespace AdoDotNetMvc.ViewModel
{
    public class DepartmentDetailViewModel    
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}