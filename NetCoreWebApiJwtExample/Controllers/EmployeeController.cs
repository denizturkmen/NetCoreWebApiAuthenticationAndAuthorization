using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiJwtExample.Business.Abstract;
using NetCoreWebApiJwtExample.Entities;
using NetCoreWebApiJwtExample.Models;

namespace NetCoreWebApiJwtExample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employee = _employeeService.GetAll();
            return Ok(employee);
        }

        [HttpGet("{Id}", Name = "Get")]
        public IActionResult Get(int id)
        {

            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return BadRequest("Employee is Null!!!!");
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Post([FromBody] EmployeeModel employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee is Null!!!!");
            }
            var entity = new Employee()
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary
            };

            _employeeService.Create(entity);
            return CreatedAtRoute("Get", new { Id = employee.Id }, employee);
        }

        [HttpPut("{Id}", Name = "Put")]
        public IActionResult Put([FromBody] EmployeeModel model, int id)
        {
            if (model == null)
            {
                return BadRequest("Employee is Null!!!!");
            }

            var entity = _employeeService.GetById(id);
            if (entity == null)
            {
                return BadRequest("Employee is Null!!!!");
            }

            entity.Name = model.Name;
            entity.Salary = model.Salary;

            _employeeService.Update(entity);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int id)
        {
            Employee employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return BadRequest("Employee is Null!!!!");
            }
            _employeeService.Delete(employee);
            return NoContent();
        }
    }
}