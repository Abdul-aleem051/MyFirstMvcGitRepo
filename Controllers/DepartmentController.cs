using Microsoft.AspNetCore.Mvc;
using AdoDotNetMvc.Models;
using AdoDotNetMvc.Repository;
using AdoDotNetMvc.ViewModel;

namespace AdoDotNetMvc.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
            _logger = logger;

        }
        
        public IActionResult Index()
        {
            var response = _departmentRepository.GetAll();
            var departments = response.Select(dept => new DepartmentViewModel{
                Id = dept.Id,
                Name = dept.Name
            });
            return View(departments);
        }

        public IActionResult Create(Department department)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateDepartmentViewModel model)
        {
            var response = _departmentRepository.GetById(model.Id);

            if (response is not null)
            {
                ViewBag.ErrorMessage = "Department already exists!";
                return View();
            }

            var department = new Department
            {
                Name = model.Name,
                Description = model.Description
            };

            if (ModelState.IsValid)
            {
                _departmentRepository.Create(department);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        public IActionResult Edit(Guid id)
        {
            var department = _departmentRepository.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            var updateDepartment = new UpdateDepartmentViewModel
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
            };

            return View(updateDepartment);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, UpdateDepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var department = new Department
            {
                Name = model.Name,
                Description = model.Description
            };

            _departmentRepository.Edit(id, department);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Detail(Guid id)
        {
            var department = _departmentRepository.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            var model = new DepartmentDetailViewModel
            {
                Name = department.Name,
                Description = department.Description ?? "N/A"
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var department = _departmentRepository.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            _departmentRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}