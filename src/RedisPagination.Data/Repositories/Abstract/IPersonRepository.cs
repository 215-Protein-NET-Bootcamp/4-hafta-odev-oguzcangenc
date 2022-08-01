using RedisPagination.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPagination.Data
{
    public interface IPersonRepository:IBaseRepository<Person>
    {
    }
}
