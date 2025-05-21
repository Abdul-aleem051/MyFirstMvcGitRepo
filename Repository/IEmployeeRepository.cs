using AdoDotNetMvc.ViewModel;

namespace AdoDotNetMvc.Repository
{
    public interface IEmployeeRepository
    {
        Task Create(CreateEmployeeViewModel employeeModel);

        Task<List<EmployeeViewModel>?> GetAllEmployeesAsync();

        EmployeeViewModel? SearchById(Guid id);

        EmployeeViewModel? SearchByName(string name);

        void Edit(UpdateEmployeeViewModel employeeModel);

        UpdateEmployeeViewModel? GetEmployeeForUpdate(Guid id);
        
        void Delete(Guid id);
    }
}