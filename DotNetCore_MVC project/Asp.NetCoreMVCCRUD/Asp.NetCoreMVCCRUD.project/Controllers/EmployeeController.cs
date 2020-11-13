using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asp.NetCoreMVCCRUD.project.Models;
using Asp.NetCoreMVCCRUD.project.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Asp.NetCoreMVCCRUD.project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(ILogger<EmployeeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // GET: Employee
        public async Task<IActionResult> GetAllEmployees()
        {
            var emp = await _unitOfWork.employeeRepository.GetAllEmployees();
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

                var employee = await _unitOfWork.employeeRepository.GetEmployeeById(id);

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
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.employeeRepository.AddEmployeeAsync(employee);
                    _unitOfWork.Commit();
                    return RedirectToAction(nameof(GetAllEmployees));
                }
                return View(employee);
            }
            catch(Exception)
            {
                _unitOfWork.Rollback();
                return RedirectToAction(nameof(GetAllEmployees));
            }
            
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> UpdateEmployee(int? id)
        {
            try
            {
                var employee = await _unitOfWork.employeeRepository.GetEmployeeById(id);
                //_unitOfWork.Commit();
                return View(employee);
            }
            catch(Exception e)
            {
                //_unitOfWork.Rollback();
                throw e;
            }
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
                    await _unitOfWork.employeeRepository.UpdateEmployee(employee);
                    _unitOfWork.Commit();
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    _unitOfWork.Rollback();
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
            try
            {
                await _unitOfWork.employeeRepository.DeleteEmployee(id);
                _unitOfWork.Commit();

                return RedirectToAction(nameof(GetAllEmployees));
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return RedirectToAction(nameof(GetAllEmployees));
            }
           
        }

      

        private bool EmployeeExists(int id)
        {
            var emp = _unitOfWork.employeeRepository.GetEmployeeById(id);

            if (emp != null)
                return true;
            else return false;
        }
    }
}
