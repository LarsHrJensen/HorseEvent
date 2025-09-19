using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using ClubContext.Application.DTOs;

public static class ExternalPostalService
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private const string ApiKey = "1ceba3d86feb4316b30b409ffbe86d9b";

    public static async Task<List<PostalCodeDTO>> GetPostalCodesAsync(string countryCode, int limit = 50)
    {
        var url = $"https://api.geoapify.com/v1/postcode/list?&countrycode={countryCode}&limit={limit}&geometry=original&apiKey={ApiKey}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var geoJson = JsonSerializer.Deserialize<GeoJsonResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        var postalCodes = geoJson.Features.Select(f => new PostalCodeDTO
        {
            PostalCode = f.Properties.Postcode,
            City = f.Properties.City,
            Municipality = f.Properties.Municipality ?? f.Properties.County,
            State = f.Properties.State
        }).ToList();

        return postalCodes;
    }

    private class GeoJsonResponse
    {
        public List<Feature> Features { get; set; }
    }

    private class Feature
    {
        public Properties Properties { get; set; }
    }

    private class Properties
    {
        public string Postcode { get; set; }
        public string City { get; set; }
        public string Municipality { get; set; }
        public string State { get; set; }
        public string County { get; set; }
    }
}
