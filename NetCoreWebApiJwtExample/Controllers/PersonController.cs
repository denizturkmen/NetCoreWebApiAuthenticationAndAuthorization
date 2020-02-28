using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiJwtExample.Business.Abstract;
using NetCoreWebApiJwtExample.Entities;
using NetCoreWebApiJwtExample.Models;

namespace NetCoreWebApiJwtExample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var person = await _personService.GetAll();
            if (person == null)
            {
                return BadRequest("Yok Brooo");
            }

            return Ok(person);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _personService.GetById(id);
            if (person == null)
            {
                return BadRequest("Yok Brooo");
            }

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonModel person)
        {
            if (person == null)
            {
                return NotFound();
            }
            var entity = new Person()
            {
                Name = person.Name,
                Department = person.Department
            };
            await _personService.Create(entity);

            return CreatedAtRoute("Gets", new { Id = person.Id }, person);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PersonModel person)
        {
            if (person == null)
            {
                return BadRequest("yok");
            }

            Person entity = await _personService.GetById(id);
            if (entity == null)
            {
                return BadRequest("yokkkk");
            }

            entity.Name = person.Name;
            entity.Department = person.Department;

            await _personService.Update(entity);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           Person person = await _personService.GetById(id);

            if (person == null)
            {
                return NotFound("Kayitlı yokk");
            }
            await _personService.Delete(person);
            return NoContent();
        }
    }
}