using AATMODELS.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace AAT.Services
{
    public class EventRegistrationClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EventRegistrationClientService> _logger;

        public EventRegistrationClientService(HttpClient httpClient, ILogger<EventRegistrationClientService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public class RegistrationFailedException : Exception
        {
            public RegistrationFailedException(string message) : base(message) { }
        }
        public async Task<EventDto> GetEventInfo(int eventId)
        {
            try
            {
                // Make an HTTP request to fetch event information
                var response = await _httpClient.GetFromJsonAsync<EventDto>($"api/events/{eventId}");

                if (response != null)
                {
                    return response;
                }
                else
                {
                    // Handle the case where the response is null
                    throw new Exception("Event information not found.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                throw ex;
            }
        }
        public async Task<List<RegistrationDto>> GetEventRegistrations(int eventId)
        {
            try
            {
                // Make an HTTP request to fetch event registrations
                var responseContent = await _httpClient.GetStringAsync($"api/registrations/event/{eventId}");

                if (string.IsNullOrWhiteSpace(responseContent))
                {
                    return new List<RegistrationDto>();
                }

                var registrations = JsonSerializer.Deserialize<List<RegistrationDto>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (registrations != null)
                {
                    return registrations;
                }
                else
                {
                    return new List<RegistrationDto>();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                throw ex;
            }
        }

        public async Task<List<RegistrationDto>> GetRegistrationsAsync()
        {
            try
            {
                var registrations = await _httpClient.GetFromJsonAsync<List<RegistrationDto>>("api/registrations");
                return registrations ?? new List<RegistrationDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching registrations.");
                throw;
            }
        }

        public async Task<bool> CheckUserRegistration(int eventId, Guid userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/registrations/check/{eventId}/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    var isRegistered = await response.Content.ReadFromJsonAsync<bool>();
                    return isRegistered;
                }
                else
                {
                    throw new Exception($"Failed to check user registration: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking user registration.");
                throw;
            }
        }

        public async Task<RegistrationDto> RegisterForEvent(RegistrationDto registration)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/registrations", registration);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Log the raw response content
                    _logger.LogInformation($"Raw API Response: {responseContent}");

                    // Validate JSON using a JSON validator (Step 4)
                    // You can manually copy the JSON content from the log and validate it online.

                    return await response.Content.ReadFromJsonAsync<RegistrationDto>();
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Registration failed with BadRequest: {errorMessage}");
                    throw new RegistrationFailedException($"Registration failed: {errorMessage}");
                }
                else
                {
                    // Handle other non-success status codes
                    _logger.LogError($"Registration failed with status code {response.StatusCode}");
                    throw new Exception($"Registration failed with status code {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RegisterForEvent method");
                throw; // Re-throw the exception to propagate it up the call stack
            }
        }
    }
}
