using Asp.NetCoreMVCCRUD.project.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeContext _employeeContext;
        private IEmployeeRepository _employeeRepository;

        public UnitOfWork(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public IEmployeeRepository employeeRepository
        {
            get { return _employeeRepository = _employeeRepository ?? new EmployeeRepository(_employeeContext); }
        }

    

        public void Commit()
        {
            _employeeContext.SaveChanges();
        }

        public void Rollback()
        {
            _employeeContext.Dispose();
        }
    }
}
