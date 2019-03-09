using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using POS.Api.Models;
using POS.Api.Repositories.Interfaces;
using POS.Api.Utilities;

namespace POS.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    [EnableCors(Utility.CORE_POLICY)]
    public class CustomerController : Controller
    {
        ICustomerRepository _Repository;
        public CustomerController(ICustomerRepository repository)
        {
            _Repository = repository;
        }

        [HttpGet("{firstname}/{surname}/{postcode}")]
        public IActionResult Search(string firstname, string surname, string postcode)
        {
            try
            {
                return Ok(_Repository.Search(firstname, surname, postcode));
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
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
                throw new System.Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            try
            {
                Customer customerToReturn = _Repository.GetById(id);

                if (customerToReturn != null)
                {
                    return Ok(customerToReturn);
                }

                return NotFound();

            }
            catch(System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        [HttpPost()]
        public IActionResult Insert([FromBody] Customer customer)
        {
            try
            {
                var newEmployee = _Repository.insert(customer);
                return Ok(newEmployee);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        [HttpPut()]
        public IActionResult Update([FromBody] Customer customer)
        {
            try
            {
                var updatedCustomer = _Repository.update(customer);
                return Ok(updatedCustomer);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
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
                    return Ok("Record archived successfully.");
                }
                return NotFound();

            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

    }
}