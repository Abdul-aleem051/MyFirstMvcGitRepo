using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdoDotNetMvc.Models;
using AdoDotNetMvc.Repository;
using System.Threading.Tasks;
using AdoDotNetMvc.ViewModel;

namespace AdoDotNetMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEmployeeRepository _employeeRepository;

    public HomeController(ILogger<HomeController> logger, IEmployeeRepository employeeRepository)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await _employeeRepository.GetAllEmployeesAsync();
        return View(employees);
    }

    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    public ActionResult Create(CreateEmployeeViewModel createEmployee)
    {
        var employee = _employeeRepository.SearchByName(createEmployee.FirstName);

        if (employee is not null)
        {
            ViewBag.ErrorMessage = "employee  exists!";
            return View();
        }

        if (ModelState.IsValid)
        {
            _employeeRepository.Create(createEmployee);
            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    public IActionResult Update(Guid id)
    {
        var employee = _employeeRepository.SearchById(id);

        if (employee == null)
        {
            return NotFound();
        }

        var updateEmployee = new UpdateEmployeeViewModel
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Department = employee.Department,
            Salary = employee.Salary,
            HireDate = DateTime.Parse(employee.HireDate)

        };

        return View(updateEmployee);
    }

    [HttpPost]
    public IActionResult Update(UpdateEmployeeViewModel updateEmployee)
    {
        if (!ModelState.IsValid)
        {
            return View(updateEmployee);
        }

        _employeeRepository.Edit(updateEmployee);

        return RedirectToAction(nameof(Index));
    }


    [HttpGet("Detail/{id}")]
    public ActionResult Detail(Guid id)
    {
        var employee = _employeeRepository.SearchById(id);

        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }


    [HttpPost]
    public IActionResult Delete(Guid id)
    {
        var employee = _employeeRepository.SearchById(id);

        if (employee == null)
        {
            return NotFound();
        }

        _employeeRepository.Delete(id);

        return RedirectToAction(nameof(Index));
    }

}
