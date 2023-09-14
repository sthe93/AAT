using AATMODELS.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AAT.Services
{
    public class EventClientService
    {
        private readonly HttpClient _httpClient;

        public EventClientService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<EventDto>> GetEventsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EventDto>>("api/events");
        }

        public async Task<EventDto?> CreateEventAsync(EventDto eventDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/events", eventDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EventDto>();
            }
            else
            {
                throw new Exception("Failed to create the event.");
            }
        }

        public async Task<EventDto> GetEventByIdAsync(int eventId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/events/{eventId}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<EventDto>();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    // Handle the case when the event with the specified ID is not found
                    // You can throw a custom exception or return null, depending on your needs
                    // For example, log the error and return null
                    LogError($"Event with ID {eventId} not found");
                    return null;
                }
                else
                {
                    // Handle other HTTP error status codes
                    // You can throw a custom exception or return null, depending on your needs
                    // For example, log the HTTP error and return null
                    LogError($"HTTP error: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request-related exceptions (e.g., network issues)
                // Log the exception or throw a custom exception, depending on your needs
                // For example, log the exception and return null
                LogError("Failed to retrieve event", ex);
                return null;
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                // Log the exception or throw a custom exception, depending on your needs
                // For example, log the exception and return null
                LogError("An error occurred while retrieving the event", ex);
                return null;
            }
        }

        private void LogError(string message, Exception exception = null)
        {
            // Implement your logging logic here, e.g., using a logging library like Serilog or ILogger
            // Example using Serilog: Log.Error(exception, message);
            // Example using ILogger: _logger.LogError(exception, message);
        }

        public async Task<EventDto> UpdateEventAsync(EventDto eventDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/events/{eventDto.Id}", eventDto);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<EventDto>();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to update the event. Status code: {response.StatusCode}. Error message: {errorMessage}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request-related exceptions (e.g., network issues)
                // You can log the exception or throw a custom exception with a specific message
                throw new Exception($"Failed to update the event due to a network issue. Error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                // You can log the exception or throw a custom exception with a specific message
                throw new Exception($"An error occurred while updating the event. Error: {ex.Message}", ex);
            }
        }


        public async Task<bool> DeleteEventAsync(int eventId)
        {
            var response = await _httpClient.DeleteAsync($"api/events/{eventId}");
            return response.IsSuccessStatusCode;
        }
    }
}
