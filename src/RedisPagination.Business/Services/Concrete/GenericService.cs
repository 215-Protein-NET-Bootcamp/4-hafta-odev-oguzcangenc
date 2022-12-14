using AutoMapper;
using RedisPagination.Business.Constants;
using RedisPagination.Core;
using RedisPagination.Data;

namespace RedisPagination.Business
{
    public class GenericService<TDto, TEntity> : IGenericService<TDto, TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;
        protected readonly IMapper Mapper;
        protected readonly IUnitOfWork UnitOfWork;

        public GenericService(IGenericRepository<TEntity> genericRepository, IMapper mapper, IUnitOfWork unitOfWork) : base()
        {
            _genericRepository = genericRepository;
            this.Mapper = mapper;
            this.UnitOfWork = unitOfWork;
        }
        public async Task<IDataResult<IEnumerable<TDto>>> GetAllAsync()
        {
            var tempEntity = await _genericRepository.GetAllAsync();

            var result = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(tempEntity);

            return new SuccessDataResult<IEnumerable<TDto>>(result, Messages.GETALL_DATA);
        }

        public async Task<IDataResult<TDto>> GetByIdAsync(int id)
        {
            var tempEntity = await _genericRepository.GetByIdAsync(id);

            var result = Mapper.Map<TEntity, TDto>(tempEntity);

            return new SuccessDataResult<TDto>(result, Messages.GET_DATA);
        }

        public async Task<IDataResult<TDto>> InsertAsync(TDto insertResource)
        {
            try
            {
                // Mapping Resource to Entity
                var tempEntity = Mapper.Map<TDto, TEntity>(insertResource);

                await _genericRepository.InsertAsync(tempEntity);
                await UnitOfWork.CompleteAsync();

                return new SuccessDataResult<TDto>(Mapper.Map<TEntity, TDto>(tempEntity), Messages.INSERT_DATA);
            }
            catch (Exception ex)
            {
                throw new MessageResultException(Messages.ADD_DATA_ERROR, ex);
            }
        }

        public async Task<IDataResult<TDto>> RemoveAsync(int id)
        {
            try
            {
                // Validate Id is existent
                var tempEntity = await _genericRepository.GetByIdAsync(id);
                if (tempEntity is null)
                    return new ErrorDataResult<TDto>(Messages.GET_NO_ID_DATA);

                _genericRepository.Remove(tempEntity);
                await UnitOfWork.CompleteAsync();

                return new SuccessDataResult<TDto>(Mapper.Map<TEntity, TDto>(tempEntity), Messages.DELETE_DATA);
            }
            catch (Exception ex)
            {
                throw new MessageResultException(Messages.DELETE_DATA_ERROR, ex);
            }
        }

        public async Task<IDataResult<TDto>> UpdateAsync(int id, TDto updateResource)
        {
            try
            {
                var tempEntity = await _genericRepository.GetByIdAsync(id);
                if (tempEntity is null)
                    return new ErrorDataResult<TDto>(Messages.GET_NO_ID_DATA);

                Mapper.Map(updateResource, tempEntity);

                await UnitOfWork.CompleteAsync();

                var resource = Mapper.Map<TEntity, TDto>(tempEntity);

                return new SuccessDataResult<TDto>(resource, Messages.UPDATE_DATA);
            }
            catch (Exception ex)
            {
                throw new MessageResultException(Messages.UPDATE_DATA_ERROR, ex);
            }
        }
    }
}
