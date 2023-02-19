using DAL;

namespace BL;
public interface ICountry
{
    Task<List<CountryDTO>> AllCountries();
    Task<bool> AddCountries();
    Task<Country> GetCountry(string code);
    Task<bool> DeleteCountry(string code);
}
