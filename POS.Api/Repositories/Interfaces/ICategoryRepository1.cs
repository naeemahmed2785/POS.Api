using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace POS.Api.Repositories.Interfaces
{
    public interface ICategoryRepository<T> where T : class
    {
        T insert(T t);
        T update(T t);
        bool Delete(int id);
        ICollection<T> Getall();
        T GetById(int id);

    }
}