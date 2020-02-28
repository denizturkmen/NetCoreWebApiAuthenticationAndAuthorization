﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWebApiJwtExample.DataAccess.Abstract;
using NetCoreWebApiJwtExample.Entities;

namespace NetCoreWebApiJwtExample.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfCoreEmployeeDal:EfCoreGenericRepository<Employee,DatabaseContext> ,IEmployeeDal
    {
    }
}
