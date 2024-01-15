using CRUDwebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDwebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeController(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees() {
            var allEmployees = _employeeContext.Employees;

            if (allEmployees == null) {
                return NotFound();
            }
            return allEmployees.ToList();
        }
        
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id) {
            var employee = _employeeContext.Employees.Find(id);

            if(employee == null) {
                return NotFound();
            }
            return employee;
        }
        [HttpPost]
        public ActionResult<Employee> CreateEmployee(Employee employee) {
            if(employee == null) {
                return BadRequest();
            }
            _employeeContext.Employees.Add(employee);
            _employeeContext.SaveChanges();

            return CreatedAtAction(nameof(GetEmployee) , new { id = employee.Id} , employee);
        } 

    }
}
