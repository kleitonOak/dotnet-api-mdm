using AutoMapper;
using Data.Dtos;
using Models;

namespace Profiles;

public class PersonProfile: Profile
{
    public PersonProfile()
    {
        CreateMap<CreatePersonDto, Person>();
        CreateMap<UpdatePersonDto, Person>()
        .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Person,UpdatePersonDto>();
        CreateMap<Person, ReadPersonDto>();
    }
}