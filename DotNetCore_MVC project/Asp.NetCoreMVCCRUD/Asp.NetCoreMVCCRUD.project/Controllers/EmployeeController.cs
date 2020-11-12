using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asp.NetCoreMVCCRUD.project.Models;
using Asp.NetCoreMVCCRUD.project.Repository;

namespace Asp.NetCoreMVCCRUD.project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

        // GET: Employee
        public async Task<IActionResult> GetAllEmployees()
        {
            var emp = await employeeRepository.GetAllEmployees();
            return View(emp);
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> GetEmployeeById(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var employee = await employeeRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound();
                }

                return View(employee);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        // GET: Employee/Create
        public IActionResult AddEmployee()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee([Bind("ID,Name,Designation,Joining_Date")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await employeeRepository.AddEmployeeAsync(employee);
                return RedirectToAction(nameof(GetAllEmployees));
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> UpdateEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployee(int id, [Bind("ID,Name,Designation,Joining_Date")] Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await employeeRepository.UpdateEmployee(employee);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GetAllEmployees));
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> DelEmployee(int? id)
        {


           await employeeRepository.DeleteEmployee(id);


            return RedirectToAction(nameof(GetAllEmployees));
        }

      

        private bool EmployeeExists(int id)
        {
            var emp = employeeRepository.GetEmployeeById(id);

            if (emp != null)
                return true;
            else return false;
        }
    }
}
