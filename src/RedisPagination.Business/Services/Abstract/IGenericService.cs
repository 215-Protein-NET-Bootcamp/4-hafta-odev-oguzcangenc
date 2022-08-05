using RedisPagination.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPagination.Business
{
    public interface IGenericService<TDto,TEntity>
    {
        Task<IDataResult<TDto>> GetByIdAsync(int id);
        Task<IDataResult<IEnumerable<TDto>>> GetAllAsync();
        Task<IDataResult<TDto>> InsertAsync(TDto insertResource);
        Task<IDataResult<TDto>> UpdateAsync(int id, TDto updateResource);
        Task<IDataResult<TDto>> RemoveAsync(int id);
    }
}
