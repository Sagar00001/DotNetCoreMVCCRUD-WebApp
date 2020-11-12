using Asp.NetCoreMVCCRUD.project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.project.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployees();

        Task<Employee> GetEmployeeById(int? Id);

        Task<int> AddEmployeeAsync(Employee emp);

        Task DeleteEmployee(int? Id);

        Task UpdateEmployee(Employee emp);
    }
}
