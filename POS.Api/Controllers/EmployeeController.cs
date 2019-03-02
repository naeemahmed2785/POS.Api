using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using POS.Api.Models;
using POS.Api.Repositories.Interfaces;
using POS.Api.Utilities;

namespace POS.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Employee")]
    [EnableCors(Utility.CORE_POLICY)]
    public class EmployeeController : Controller
    {
        IEmployeeRepository _Repository;
        public EmployeeController(IEmployeeRepository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_Repository.GetAll());
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            try
            {
                Employee EmployeeToReturn = _Repository.GetById(id);

                if (EmployeeToReturn != null)
                {
                    return Ok(EmployeeToReturn);
                }

                return NotFound();

            }
            catch (System.Exception ex)
            {
                return NoContent();
            }
        }

        [HttpPost()]
        public IActionResult Insert([FromBody] Employee employee)
        {
            try
            {
                var newEmployee = _Repository.insert(employee);
                return Ok(newEmployee);
            }
            catch (System.Exception ex)
            {
                return NoContent();
            }
        }

        [HttpPut()]
        public IActionResult Update([FromBody] Employee employee)
        {
            try
            {
                var updatedStudent = _Repository.update(employee);
                return Ok(updatedStudent);
            }
            catch (System.Exception ex)
            {
                return NoContent();
            }
        }

        [EnableCors(Utility.CORE_POLICY)]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_Repository.Delete(id))
                {
                    return Ok("Record archived successfull.");
                }
                return NotFound();

            }
            catch (System.Exception ex)
            {
                return NoContent();
            }
        }

    }
}