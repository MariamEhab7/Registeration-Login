using AutoMapper;
using DAL;

namespace BL;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, ReadUserDTO>();
        CreateMap<AddUserDTO, User>().ForMember(src=>src.Photo, opt=>opt.Ignore());

        CreateMap<LoginDTO, User>();
        CreateMap<CountryDTO, Country>();
        CreateMap<Country, ListCountriesDTO>();
    }
}
