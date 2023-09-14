
using AATAPI.Entities;
namespace AATAPI.Repositories
{
    public interface IEventsRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int eventId);
        Task AddEventAsync(Event eventToAdd);
        Task UpdateEventAsync(Event eventToUpdate);
        Task DeleteEventAsync(int eventId);
    }
}
