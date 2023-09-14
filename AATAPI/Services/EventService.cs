using AATAPI.Entities;
using AATAPI.Repositories;
using System;
using System.Threading.Tasks;

namespace AATAPI.Services
{
    public class EventService
    {
        private readonly IEventsRepository _eventsRepository;

        public EventService(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventsRepository.GetAllEventsAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            return await _eventsRepository.GetEventByIdAsync(eventId);
        }

        public async Task AddEventAsync(Event eventToAdd)
        {
            await _eventsRepository.AddEventAsync(eventToAdd);
        }

        public async Task UpdateEventAsync(Event eventToUpdate)
        {
            await _eventsRepository.UpdateEventAsync(eventToUpdate);
        }

        public async Task DeleteEventAsync(int eventId)
        {
            await _eventsRepository.DeleteEventAsync(eventId);
        }
    }
}