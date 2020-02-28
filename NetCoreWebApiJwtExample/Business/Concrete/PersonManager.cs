using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWebApiJwtExample.Business.Abstract;
using NetCoreWebApiJwtExample.DataAccess.Abstract;
using NetCoreWebApiJwtExample.Entities;

namespace NetCoreWebApiJwtExample.Business.Concrete
{
    public class PersonManager:IPersonService
    {

        private IPersonDal _personDal;

        public PersonManager(IPersonDal personDal)
        {
            _personDal = personDal;
        }
        public async Task<List<Person>> GetAll()
        {
            return await _personDal.GetAll();
        }

        public async Task<Person> GetById(int id)
        {
            return await _personDal.GetById(id);
        }

        public async Task Create(Person entity)
        {
            await _personDal.Create(entity);
        }

        public async Task Update(Person entity)
        {
            await _personDal.Update(entity);
        }

        public async Task Delete(Person entity)
        {
            await _personDal.Delete(entity);
        }
    }
}
