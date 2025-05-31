using AdoDotNetMvc.Models;

namespace AdoDotNetMvc.Repository
{
    public interface IDepartmentRepository
    {
        public List<Department> GetAll();
        public Department GetById(Guid id);
        public void Create(Department department);
        public void Edit(Guid id, Department department);
        public void Delete(Guid id);
    }
}