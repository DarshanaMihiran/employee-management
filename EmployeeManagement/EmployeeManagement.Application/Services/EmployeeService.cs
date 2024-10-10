using EmployeeManagement.Application.DTO;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Position = e.Position
            }).ToList();
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Position = employee.Position
            };
        }

        public async Task AddEmployeeAsync(EmployeeDto employee)
        {
            var entity = new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Position = employee.Position
            };
            await _employeeRepository.AddAsync(entity);
        }

        public async Task UpdateEmployeeAsync(EmployeeDto employee)
        {
            var entity = new Employee
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Position = employee.Position
            };
            await _employeeRepository.UpdateAsync(entity);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }
    }
}
