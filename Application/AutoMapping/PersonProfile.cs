using AutoMapper;
using Application.DTO;
using Domain.Models;

namespace Application.AutoMapping
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonModel, PersonDTO>()
                .ReverseMap();
        }
    }
}
