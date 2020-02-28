using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWebApiJwtExample.Entities;

namespace NetCoreWebApiJwtExample.Business.Abstract
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
        Employee GetById(int id);
        void Create(Employee entity);
        void Update(Employee entity);
        void Delete(Employee entity);
    }
}
