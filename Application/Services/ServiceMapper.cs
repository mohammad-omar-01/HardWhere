using AutoMapper;
using Domain.User;
using Domain.UserType;

namespace Application.Services
{
    public class ServiceMapper : Profile
    {
        public static IMapper Configure()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, CustomerDTO>();
                cfg.CreateMap<CustomerDTO, User>();
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
