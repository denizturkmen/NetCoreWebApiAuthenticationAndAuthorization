using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWebApiJwtExample.Entities;

namespace NetCoreWebApiJwtExample.Business.Abstract
{
    public interface IPersonService
    {
        Task<List<Person>> GetAll();
        Task<Person> GetById(int id);

        Task Create(Person entity);
        Task Update(Person entity);
        Task Delete(Person entity);
    }
}
