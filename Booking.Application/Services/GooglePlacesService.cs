using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class GooglePlacesService(HttpClient httpClient, IConfiguration config) : IGooglePlacesService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _apiKey = config["GoogleApi:PlacesApiKey"];

        public async Task<PlaceDetailsDto> GetPlaceDetailsAsync(string placeId)
        {
            var url = $"https://maps.googleapis.com/maps/api/place/details/json?placeid={placeId}&key={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var googleResponse = JsonSerializer.Deserialize<GooglePlaceDetailsResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var r = googleResponse?.Result;

            return new PlaceDetailsDto
            {
                PlaceId = r?.Place_Id,
                Name = r?.Name,
                FormattedAddress = r?.Formatted_Address,
                PhoneNumber = r?.Formatted_Phone_Number,
                Website = r?.Website,
                Latitude = r?.Geometry?.Location?.Lat ?? 0,
                Longitude = r?.Geometry?.Location?.Lng ?? 0,
                Rating = r?.Rating
            };
        }
    }
}
