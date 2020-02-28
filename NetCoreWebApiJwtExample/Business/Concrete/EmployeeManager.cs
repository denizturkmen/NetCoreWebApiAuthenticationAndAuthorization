using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWebApiJwtExample.Business.Abstract;
using NetCoreWebApiJwtExample.DataAccess.Abstract;
using NetCoreWebApiJwtExample.Entities;

namespace NetCoreWebApiJwtExample.Business.Concrete
{
    public class EmployeeManager:IEmployeeService
    {
        private IEmployeeDal _employeeDal;

        public EmployeeManager(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }

        public List<Employee> GetAll()
        {
            return _employeeDal.GetAll();
        }

        public Employee GetById(int id)
        {
            return _employeeDal.GetById(id);
        }

        public void Create(Employee entity)
        {
            _employeeDal.Create(entity);
        }

        public void Update(Employee entity)
        {
           _employeeDal.Update(entity);
        }

        public void Delete(Employee entity)
        {
          _employeeDal.Delete(entity);
        }
    }
}
