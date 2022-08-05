

using AutoMapper;
using RedisPagination.Entities;

namespace RedisPagination.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDto, Employee>().ReverseMap();

        }
    }
}
