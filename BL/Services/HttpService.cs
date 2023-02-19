using Newtonsoft.Json;

namespace BL;

public class HttpService
{
    private readonly HttpClient _httpClient = new HttpClient();
    
    public async Task<List<CountryDTO>> GetHttpRequest()
    {
        try
        {
            var endpoint = new Uri("https://gist.githubusercontent.com/anubhavshrimal/75f6183458db8c453306f93521e93d37/raw/f77e7598a8503f1f70528ae1cbf9f66755698a16/CountryCodes.json");
            var result = _httpClient.GetAsync(endpoint).Result;
            string? jsonString = result.Content.ReadAsStringAsync().Result;
            dynamic responseObject = JsonConvert.DeserializeObject<List<CountryDTO>>(jsonString);

            return responseObject;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }

    }
}
