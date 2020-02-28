using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWebApiJwtExample.Entities;

namespace NetCoreWebApiJwtExample.DataAccess.Abstract
{
    public interface IEmployeeDal:IRepository<Employee>
    {

    }
}
