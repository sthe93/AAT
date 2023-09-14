using System;
using System.Collections.Generic;
using System.Linq;
using AATAPI.Entities;
using AATAPI.Data;
using Microsoft.EntityFrameworkCore;
using AATAPI.Repositories;

namespace AATAPI.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly AppDbContext _context;

        public EventsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            return await _context.Events.FindAsync(eventId);
        }

        public async Task AddEventAsync(Event eventToAdd)
        {
            _context.Events.Add(eventToAdd);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateEventAsync(Event eventToUpdate)
        {
            _context.Entry(eventToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task DeleteEventAsync(int eventId)
        {
            var eventToDelete = await _context.Events.FindAsync(eventId);
            if (eventToDelete != null)
            {
                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
