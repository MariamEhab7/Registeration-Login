using AutoMapper;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace BL;

public class CountryService : ICountry
{
    #region Dependancy Injection
    private readonly HttpService _httpService;
    private readonly UserContext _context;
    private readonly IMapper _mapper;

    public CountryService(HttpService httpService, UserContext context, IMapper mapper)
    {
        _httpService = httpService;
        _context = context;
        _mapper = mapper;
    }
    #endregion
    public async Task<List<CountryDTO>> AllCountries()
    {
        var countries = await _httpService.GetHttpRequest();
        return countries;
    }


    public async Task<bool> AddCountries()
    {
        try
        {
            var jsonObject = await AllCountries();
            var countriesList = jsonObject;
            var dbList = _mapper.Map<ICollection<Country>>(countriesList);
            await _context.Countries.AddRangeAsync(dbList);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return false;
        }
    }

    public async Task<bool> DeleteCountry(string code)
    {
        var country = _context.Countries.Where(c => c.Code == code);
        if (country == null)
            return false;
        
        _context.Remove(country);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<Country> GetCountry(string code)
    {
        var country = await _context.Countries.FirstOrDefaultAsync(c => c.Code == code);
        if (country == null)
            return null;
        
        return country;
    }


}
