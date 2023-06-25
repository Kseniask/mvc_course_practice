using AutoMapper;
using Vidly.DTOs;

namespace Vidly.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Movie, MovieDTO>();
            CreateMap<MovieDTO, Movie>();
            CreateMap<MembershipType, MembershipTypeDTO>();
        }
    }
}
