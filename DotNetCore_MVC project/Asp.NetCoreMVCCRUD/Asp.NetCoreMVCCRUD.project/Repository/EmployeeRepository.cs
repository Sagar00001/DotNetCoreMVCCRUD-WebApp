using Asp.NetCoreMVCCRUD.project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.project.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        EmployeeContext employeeContext;
        public EmployeeRepository(EmployeeContext _employeeContext)
        {
            employeeContext = _employeeContext;
        }
        public async Task<int> AddEmployeeAsync(Employee emp)
        {
            if (employeeContext != null)
            {
                await employeeContext.Employees.AddAsync(emp);
                await employeeContext.SaveChangesAsync();
                return emp.ID;
            }
            return 0;
        }

        public async Task DeleteEmployee(int? Id)
        {
           

            if (employeeContext != null)
            {
                //Find the employee for specific employee id
                var emp = await employeeContext.Employees.FirstOrDefaultAsync(x => x.ID == Id);
                if (emp != null)
                {
                    //Delete that employee
                    employeeContext.Employees.Remove(emp);
                    //Commit the transaction
                   await employeeContext.SaveChangesAsync();
                }
                
            }
            
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            if (employeeContext != null)
            {
                return await employeeContext.Employees.ToListAsync();
            }
            return null;
        }

        public async Task<Employee> GetEmployeeById(int? Id)
        {
            if (employeeContext != null)
            {
                return await employeeContext.Employees.FirstOrDefaultAsync(x => x.ID == Id);
            }
            return null;
        }

        public async Task UpdateEmployee(Employee emp)
        {
            if (employeeContext != null)
            {
                //Delete that employee
                employeeContext.Employees.Update(emp);
                //Commit the transaction
                await employeeContext.SaveChangesAsync();
            }
        }
    }
}
