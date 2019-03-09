using POS.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Api.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        ICollection<Customer> Search(string firstName, string surName, string postcode);
    }
}
