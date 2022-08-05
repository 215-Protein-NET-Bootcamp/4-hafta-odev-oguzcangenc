using AutoMapper;
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

            return new SuccessDataResult<IEnumerable<TDto>>(result);
        }

        public async Task<IDataResult<TDto>> GetByIdAsync(int id)
        {
            var tempEntity = await _genericRepository.GetByIdAsync(id);

            var result = Mapper.Map<TEntity, TDto>(tempEntity);

            return new SuccessDataResult<TDto>(result);
        }

        public async Task<IDataResult<TDto>> InsertAsync(TDto insertResource)
        {
            try
            {
                // Mapping Resource to Entity
                var tempEntity = Mapper.Map<TDto, TEntity>(insertResource);

                await _genericRepository.InsertAsync(tempEntity);
                await UnitOfWork.CompleteAsync();

                return new SuccessDataResult<TDto>(Mapper.Map<TEntity, TDto>(tempEntity));
            }
            catch (Exception ex)
            {
                throw new MessageResultException("Saving_Error", ex);
            }
        }

        public async Task<IDataResult<TDto>> RemoveAsync(int id)
        {
            try
            {
                // Validate Id is existent
                var tempEntity = await _genericRepository.GetByIdAsync(id);
                if (tempEntity is null)
                    return new ErrorDataResult<TDto>("Id_NoData");

                await _genericRepository.RemoveAsync(tempEntity);
                await UnitOfWork.CompleteAsync();

                return new SuccessDataResult<TDto>(Mapper.Map<TEntity, TDto>(tempEntity));
            }
            catch (Exception ex)
            {
                throw new MessageResultException("Deleting_Error", ex);
            }
        }

        public async Task<IDataResult<TDto>> UpdateAsync(int id, TDto updateResource)
        {
            try
            {
                var tempEntity = await _genericRepository.GetByIdAsync(id);
                if (tempEntity is null)
                    return new ErrorDataResult<TDto>("NoData");

                Mapper.Map(updateResource, tempEntity);

                await UnitOfWork.CompleteAsync();

                var resource = Mapper.Map<TEntity, TDto>(tempEntity);

                return new SuccessDataResult<TDto>(resource);
            }
            catch (Exception ex)
            {
                throw new MessageResultException("Updating_Error", ex);
            }
        }
    }
}
