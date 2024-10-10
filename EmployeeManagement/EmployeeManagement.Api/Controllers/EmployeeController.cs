using EmployeeManagement.Application.DTO;
using EmployeeManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();

            return Ok(employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]      
        public async Task<ActionResult> AddEmployee(EmployeeDto employeeDto)
        {
            await _employeeService.AddEmployeeAsync(employeeDto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeDto.Id }, employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.Id) return BadRequest();

            await _employeeService.UpdateEmployeeAsync(employeeDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
